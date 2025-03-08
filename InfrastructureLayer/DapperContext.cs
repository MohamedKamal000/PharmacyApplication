using System.Data;
using DomainLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer
{
    public class DapperContext : IConnection
    {
        private readonly string? connectionString;

        public DapperContext()
        {
            //var configurationBuilder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json").Build();

            //connectionString = configurationBuilder.GetConnectionString("SqlConnection");

            connectionString =
                "Server=.;Database=PharmacyApplication;User Id=sa;Password=sa123456;Encrypt=false;TrustServerCertificate=true";
        }


        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
