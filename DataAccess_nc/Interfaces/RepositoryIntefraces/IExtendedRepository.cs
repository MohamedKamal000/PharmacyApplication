using System.Collections.Generic;

namespace DataAccess
{
    public interface IExtendedRepository<TObject> : IRepository<TObject>
    {
        IEnumerable<TObject> GetAll();
    }
}