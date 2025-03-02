using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class SubMedicalCategoryRepository : GenericRepository<SubMedicalCategory>, IExtendedRepository<SubMedicalCategory>
    {
        public SubMedicalCategoryRepository(IDbConnection dbConnection, ILogger logger) : base(dbConnection, logger)
        {
        }

        public IEnumerable<SubMedicalCategory> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}