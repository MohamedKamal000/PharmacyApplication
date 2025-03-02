using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class MedicalProductsRepository : GenericRepository<MedicalProducts>, IExtendedRepository<MedicalProducts>
    {
        public MedicalProductsRepository(IDbConnection dbConnection, ILogger logger) : base(dbConnection, logger)
        {
        }

        public IEnumerable<MedicalProducts> GetAll()
        {
            // implements the get all query
            throw new System.NotImplementedException();
        }
    }
}