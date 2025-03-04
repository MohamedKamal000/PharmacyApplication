
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;


namespace DataAccess
{
    public class DapperContext : IConnection
    {
        public readonly string connectionString;

        public DapperContext()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json").Build();

            connectionString = configurationBuilder.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
