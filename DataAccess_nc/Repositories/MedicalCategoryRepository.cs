using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class MedicalCategoryRepository : GenericRepository<MedicalCategory>, IExtendedRepository<MedicalCategory>
    {
        public MedicalCategoryRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

        public IEnumerable<MedicalCategory> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}