using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class OrdersRepository : GenericRepository<Orders>, IExtendedRepository<Orders>
    {
        public OrdersRepository(IDbConnection dbConnection, ILogger logger) : base(dbConnection, logger)
        {
        }

        public IEnumerable<Orders> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}