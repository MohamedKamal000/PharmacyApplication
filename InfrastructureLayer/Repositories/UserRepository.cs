using System.Collections.Specialized;
using System.Data;
using Dapper;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository<Users>
    {
        
        public UserRepository(IConnection connection) 
            : base(connection)
        {
            
        }

        public IEnumerable<Orders> GetUserOrders(Users user)
        {
            List<Orders> orders = new List<Orders>();

            try
            {
                using (_dbConnection)
                {
                    var orderDictionary = new Dictionary<int, Orders>();

                    orders = _dbConnection.Query<Orders, MedicalProducts, Users, Orders>(
                        DBSettings.ProceduresNames.GetUserOrders.ToString(),
                        (order, product, userQ) =>
                        {
                            if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                            {
                                orderEntry = order;
                                orderEntry.Products = new List<MedicalProducts>();
                                orderEntry.User = userQ;
                                orderDictionary.Add(orderEntry.Id, orderEntry);
                            }

                            orderEntry.Products.Add(product);
                            return orderEntry;
                        },
                        new
                        {
                            UserPhoneNumber = user.PhoneNumber
                        },
                        splitOn: "ProductName, PhoneNumber"
                        , 
                        commandType: CommandType.StoredProcedure

                    ).Distinct().ToList();

                }
            }
            catch (Exception e)
            {
                throw new Exception($"GetUserOrders Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return orders;
        }
        
        public  Users? RetrieveUserCredentials(string phoneNumber)
        {
            Users ? user = new Users();
            try
            {
                using (_dbConnection)
                {
                    user = _dbConnection.Query<Users>(
                        DBSettings.ProceduresNames.UserLogin.ToString(),
                        new
                        {
                            PhoneNumber = phoneNumber
                        },
                        commandType: CommandType.StoredProcedure
                    ).SingleOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new Exception($"GetUser Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }
                
            return user;
        }


    }
}