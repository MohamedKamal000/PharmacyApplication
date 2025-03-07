using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class SubMedicalCategoryRepository : GenericRepository<SubMedicalCategory>
    {
        public SubMedicalCategoryRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

       
    }
}