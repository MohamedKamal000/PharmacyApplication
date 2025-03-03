using System.Collections.Generic;
using System.Data;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class DeliveryRepository : GenericRepository<Delivery>, IExtendedRepository<Delivery>
    {
        public DeliveryRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

        public IEnumerable<Delivery> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}