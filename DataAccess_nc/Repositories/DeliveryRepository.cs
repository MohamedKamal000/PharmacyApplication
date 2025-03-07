using System.Collections.Generic;
using System.Data;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class DeliveryRepository : GenericRepository<Delivery>
    {
        public DeliveryRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) 
            : base(dbConnection, systemTrackingLogger)
        {
        }

      
    }
}