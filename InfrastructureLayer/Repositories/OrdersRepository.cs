using Dapper;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class OrdersRepository : GenericRepository<Order>
    {
        public OrdersRepository(IConnection dbConnection) : base(dbConnection)
        {
        }


        public override IEnumerable<Order> GetAll()
        {
            IEnumerable<Order> o = [];

            string query = $"Select * From {DBSettings.Views.ShowAllOrders.ToString()}";

            try
            {
                using (_dbConnection)
                {
                    o = _dbConnection.Query<Order>(query);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"GetAll Orders Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return o;
        }
    }
}