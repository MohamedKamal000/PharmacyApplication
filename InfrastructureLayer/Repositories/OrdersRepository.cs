using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext dbConnection) : base(dbConnection)
        {
        }


        public override IEnumerable<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            
            try
            {
                using (_dbContext)
                {
                    orders = _dbContext.Orders.
                        Include(o => o.OrderStatus).
                        Include(o => o.User).
                        Distinct().
                        OrderBy(o => o.User.PhoneNumber).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"GetAll Orders Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return orders;
        }

        public ICollection<Order> GetAllWithStatus(OrderStatusEnum status)
        {
            List<Order> orders = new List<Order>();

            try
            {
                using (_dbContext)
                {
                    orders = _dbContext.Orders.
                        Include(o => o.OrderStatus).
                        Include(o => o.User)
                        .Distinct()
                        .Where(o => o.OrderStatus.Status == status.ToString())
                        .OrderBy(o => o.User.PhoneNumber)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"GetAll Orders with status {status.ToString()} Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return orders;
        }

        public int AcceptUserOrders(Users user, Delivery deliveryMan)
        {
            int result = -1;
            try
            {
                using (_dbContext)
                {
                    List<Order> orders = _dbContext.Orders.Include(o => o.User)
                        .Where(o => o.User.PhoneNumber == user.PhoneNumber).ToList();


                    foreach (var o in orders)
                    {
                        o.StatusId = (int) OrderStatusEnum.delivering;
                        o.DeliveryMan = deliveryMan;
                        _dbSet.Attach(o);
                        _dbSet.Entry(o).State = EntityState.Modified;
                    }

                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Accept User Order Failed,userID: {user.Id}, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return result;
        }

        public int DeclineUserOrder(Users user)
        {
            int result = -1;
            try
            {
                using (_dbContext)
                {
                    List<Order> orders = _dbSet.Include(o => o.User)
                        .Where(o => o.User.PhoneNumber == user.PhoneNumber).ToList();


                    foreach (var o in orders)
                    {
                        o.StatusId = (int) OrderStatusEnum.declined;
                        _dbSet.Attach(o);
                        _dbSet.Entry(o).State = EntityState.Modified;
                    }

                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Accept User Order Failed,userID: {user.Id}, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return result;
        }

        public int SetUserOrderAsDelivered(Users user)
        {
            int result = -1;
            try
            {
                using (_dbContext)
                {
                    List<Order> orders = _dbContext.Orders.Include(o => o.User)
                        .Where(o => o.User.PhoneNumber == user.PhoneNumber)
                        .Include(o => o.Product).ToList();


                    foreach (var o in orders)
                    {
                        o.StatusId = (int) OrderStatusEnum.delivered;
                        o.Product.Stock -= o.ProductAmount;
                        _dbSet.Attach(o);
                        _dbSet.Entry(o).State = EntityState.Modified;
                    }

                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Accept User Order Failed,userID: {user.Id}, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return result;
        }
    }
}