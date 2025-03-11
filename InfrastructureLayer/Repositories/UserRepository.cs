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

        public UserOder GetUserOrders(Users user)
        {
            UserOder userOrder = null;

            try
            {
                using (_dbConnection)
                {
                    _dbConnection.Query<UserOder, MedicalProducts, OrderedProducts, Users, UserOder>(
                        DBSettings.ProceduresNames.GetUserOrders.ToString(),
                        (order, medicalProduct, finalOrderedProduct, userQ) =>
                        {
                            if (userOrder == null)
                            {
                                userOrder = order;
                                userOrder.Products = new List<OrderedProducts>();
                                userOrder.User = userQ;
                            }

                            finalOrderedProduct.Product = medicalProduct;
                            userOrder.TotalPrice += finalOrderedProduct.Product.Price;
                            userOrder.Products.Add(finalOrderedProduct);


                            return userOrder;
                        },
                        new
                        {
                            UserPhoneNumber = user.PhoneNumber
                        },
                        splitOn: "ProductName, Amount, PhoneNumber", 
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception e)
            {
                throw new Exception($"GetUserOrders Failed, error Message: {e.Message}\nError Stack: {e.StackTrace}");
            }

            return userOrder;
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