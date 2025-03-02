using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class MedicalCategoryRepository : GenericRepository<MedicalCategory>, IExtendedRepository<MedicalCategory>
    {
        public MedicalCategoryRepository(IDbConnection dbConnection, ILogger logger) : base(dbConnection, logger)
        {
        }

        public IEnumerable<MedicalCategory> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}