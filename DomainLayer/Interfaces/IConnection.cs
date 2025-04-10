using System.Data;

namespace DomainLayer.Interfaces
{
    public interface IConnection
    {
        IDbConnection CreateConnection();
    }
}
