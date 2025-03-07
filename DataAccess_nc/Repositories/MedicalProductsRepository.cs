﻿using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class MedicalProductsRepository : GenericRepository<MedicalProducts>
    {
        public MedicalProductsRepository(IConnection dbConnection, ISystemTrackingLogger systemTrackingLogger) : base(dbConnection, systemTrackingLogger)
        {
        }

    }
}