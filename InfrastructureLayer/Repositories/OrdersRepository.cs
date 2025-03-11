using Dapper;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class OrdersRepository : GenericRepository<Orders>
    {
        public OrdersRepository(IConnection dbConnection) : base(dbConnection)
        {
        }


        public override IEnumerable<Orders> GetAll()
        {
            IEnumerable<Orders> o = [];

            string query = $"Select * From {DBSettings.Views.ShowAllOrders.ToString()}";

            try
            {
                using (_dbConnection)
                {
                    o = _dbConnection.Query<Orders>(query);
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