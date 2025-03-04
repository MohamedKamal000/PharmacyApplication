using System.Data;


namespace DataAccess.Interfaces
{
    public interface IConnection
    {
        IDbConnection CreateConnection();
    }
}
