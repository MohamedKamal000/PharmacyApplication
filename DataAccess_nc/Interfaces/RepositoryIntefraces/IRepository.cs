using System.Collections.Generic;

namespace DataAccess
{
    public interface IRepository<TObject>
    {
        int Add(TObject obj);
        int Update(KeyValuePair<string, object> whereClause, TObject obj);
        int Delete(KeyValuePair<string, object> whereClause);
        bool Get(KeyValuePair<string, object> whereClause, out IEnumerable<TObject> result);
    }
}