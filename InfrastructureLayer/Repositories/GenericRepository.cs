using System.Data;
using System.Reflection;
using Dapper;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Newtonsoft.Json.Linq;

namespace InfrastructureLayer.Repositories
{
    public class GenericRepository<TObject> : 
        IRepository<TObject>,IExtendedRepository<TObject> where TObject : class
    {
        // used only in Insertion
        protected readonly IDbConnection _dbConnection;
        
        public GenericRepository(IConnection dbConnection)
        {
            _dbConnection = dbConnection.CreateConnection();
        }
        
        private static string ToSqlTypeConvertor(object value)
        {
            if (value == null) return "NULL";

            switch (value)
            {
                case string s:
                    return "'" + s.Replace("'", "''") + "'"; // Escape single quotes
                case DateTime dt:
                    return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'"; // Format date
                case bool b:
                    return b ? "1" : "0"; // Convert bool to bit
                default:
                    return value.ToString(); // Numbers and other types
            }
        } 
        
        private static object CheckForDbnull(object value)
        {
            return value ?? DBNull.Value;
        }
        
        public int Add(TObject rowInserted)
        {
            int resultID = -1;
            string table = typeof(TObject).Name;
            Type tableType = typeof(TObject);
            PropertyInfo[] prop = tableType.GetProperties().Where(p => p.Name != "Id").ToArray();
            string columns = string.Join(",",prop.Select(x => x.Name));
            string values = string.Join(",",prop.Select(x => ToSqlTypeConvertor(x.GetValue(rowInserted))));

            try
            {
                resultID = _dbConnection.ExecuteScalar<int>(
                    DBSettings.ProceduresNames.InsertIntoAnyTable.ToString(),
                    new
                    {
                        TableName = table,
                        Columns = columns,
                        Values = values
                    },
                    commandType: CommandType.StoredProcedure
                );

            }
            catch (Exception e)
            {
                throw new Exception($"Insertion Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }
            
            return resultID;
        }
        
        // does not work for null values i may make one that work for nulls 
        public  bool Get(KeyValuePair<string, object> whereClauseConstruction, out IEnumerable<TObject> dt)
        {
            bool isOK = false;
            string table = typeof(TObject).Name;
            string column = whereClauseConstruction.Key;
            dt = Enumerable.Empty<TObject>();
            
            try
            {
                dt = _dbConnection.Query<TObject>(
                    DBSettings.ProceduresNames.SelectFromTable.ToString(),
                    new
                    {
                        TableName = table,
                        ColumnName = column,
                        InputValue = CheckForDbnull(whereClauseConstruction.Value)
                    },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                isOK = dt.Any();
            }
            catch (Exception e)
            {
                throw new Exception($"Reading Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }
            

            return isOK;
        }

        public  int Update(KeyValuePair<string, object> whereClause, TObject rowsUpdated)
        {
            int result = -1;
            string table = typeof(TObject).Name;
            string column = whereClause.Key;
            PropertyInfo[] prop = typeof(TObject).GetProperties().Where(p => p.Name != "Id").ToArray();

            Dictionary<string, object> updates = new Dictionary<string, object>();
            
            foreach (PropertyInfo p in prop)
            {
                var value = p.GetValue(rowsUpdated);
                if (value != null)
                {
                    updates.Add(p.Name,value);
                }
            }

            var formattedUpdates = updates.ToDictionary(
                x => x.Key, 
                x => ToSqlTypeConvertor(x.Value)
            );
            
            JObject j_object = JObject.FromObject(formattedUpdates);
            
            string jupdates = j_object.ToString();
            
            try
            {
                result = _dbConnection.Execute(
                    DBSettings.ProceduresNames.UpdateTable.ToString(),
                    new
                    {
                        TableName = table,
                        ColumnName = column,
                        Updates = jupdates,
                        UserColumnName = CheckForDbnull(whereClause.Value)
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception e)
            {
                throw new Exception($"Updating Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }
            
            return result;
        }

        public  int Delete(KeyValuePair<string, object> whereClause)
        {
            int result = -1;
            string table = typeof(TObject).Name;
            string column = whereClause.Key;

            try
            {
                result = _dbConnection.Execute(
                    DBSettings.ProceduresNames.DeleteFromTable.ToString(),
                    new
                    {
                        TableName = table,
                        ColumnName = column,
                        UserColumnInput = CheckForDbnull(whereClause.Value)
                    },
                    commandType: CommandType.StoredProcedure
                );
                
            }
            catch (Exception e)
            {
                throw new Exception($"Deleting Failed, error Message{e.Message}" 
                                    + $"Error Stack: {e.StackTrace}");
            }
            
            return result;
        }


        public bool CheckExist(KeyValuePair<string, object> whereClause)
        {
            bool isOk = false;
            string tableName = typeof(TObject).Name;
            string column = whereClause.Key;
            object value = whereClause.Value;

            try
            {
                int result = _dbConnection.ExecuteScalar<int>(
                    DBSettings.ProceduresNames.CheckRowInTable.ToString(),
                    new
                    {
                        TableName = tableName,
                        ColumnName = column,
                        Value = value
                    },
                    commandType: CommandType.StoredProcedure
                );

                isOk = result != 0;
            }
            catch (Exception e)
            {
                throw new Exception($"CheckRow Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return isOk;
        }

        public virtual IEnumerable<TObject> GetAll()
        {
            string table = typeof(TObject).Name;
            IEnumerable<TObject> result = [];

            try
            {
                result = _dbConnection.Query<TObject>(
                    DBSettings.ProceduresNames.GetAllFromTable.ToString(),
                    new
                    {
                        TableName = table
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception e)
            {
                throw new Exception($"GetAll Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return result;
        }
    }
}