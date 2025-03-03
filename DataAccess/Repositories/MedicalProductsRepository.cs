using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class MedicalProductsRepository : GenericRepository<MedicalProducts>, IExtendedRepository<MedicalProducts>
    {
        public MedicalProductsRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

        public IEnumerable<MedicalProducts> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}