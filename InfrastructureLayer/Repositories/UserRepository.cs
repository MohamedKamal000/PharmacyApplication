using System.Collections.Specialized;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository<Users>
    {
        
        public UserRepository(ApplicationDbContext connection) 
            : base(connection)
        {
            
        }


        // must get the user first from db and send it to this function
        // idk if this will be only for admins or both admins and users 
        public ICollection<Order> GetUserOrders(Users user)
        {
            List<Order> userOrder = new List<Order>();

            try
            {
                using (_dbContext)
                {
                    _dbContext.Entry(user).Collection(u => u.Orders).Load();

                    foreach (var o in user.Orders)
                    {
                        _dbContext.Entry(o).Reference(or => or.Product).Load();
                        _dbContext.Entry(o).Reference(or => or.DeliveryMan).Load();
                        _dbContext.Entry(o).Reference(or => or.OrderStatus).Load();
                    }
                }

                userOrder = user.Orders.ToList();
            }

            catch (Exception e)
            {
                throw new Exception($"Couldn't get user Orders, Error Message: {e.Message}");
            }


            return userOrder;
        }

        public  Users? RetrieveUser(string phoneNumber)
        {
            Users user;

            try
            {
                user = _dbSet.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            }
            catch (Exception e)
            {
                throw new Exception($"GetUser Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }
                
            return user;
        }

        public int AddOrders(List<Order> orders)
        {
            int result = -1;
            try
            {
                using (_dbContext)
                {
                    foreach (var o in orders)
                    {
                        _dbContext.MedicalProducts.Attach(o.Product).Entity.Stock -= o.ProductAmount;
                        _dbContext.MedicalProducts.Attach(o.Product).State = EntityState.Modified;
                        _dbContext.Orders.Add(o);
                    }

                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"AddOrders Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return result;
        }


        public bool CheckUserExistByPhone(string phoneNumber)
        {
            bool found = false;
            try
            {
                found = _dbSet.FirstOrDefault(u => u.PhoneNumber == phoneNumber) != null;
            }
            catch(Exception e)
            {
                throw new Exception($"CheckUserExistByPhone Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return found;
        }
    }
}