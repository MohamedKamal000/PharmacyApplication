using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class DeliveryRepository : GenericRepository<Delivery>, IExtendedRepository<Delivery>
    {
        public DeliveryRepository(IDbConnection dbConnection, ILogger logger) : base(dbConnection, logger)
        {
        }

        public IEnumerable<Delivery> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}