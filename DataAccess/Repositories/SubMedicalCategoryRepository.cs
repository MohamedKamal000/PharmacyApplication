using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class SubMedicalCategoryRepository : GenericRepository<SubMedicalCategory>, IExtendedRepository<SubMedicalCategory>
    {
        public SubMedicalCategoryRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

        public IEnumerable<SubMedicalCategory> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}