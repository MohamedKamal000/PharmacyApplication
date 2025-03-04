using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class OrdersRepository : GenericRepository<Orders>, IExtendedRepository<Orders>
    {
        public OrdersRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

        public IEnumerable<Orders> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}