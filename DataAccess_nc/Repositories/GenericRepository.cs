using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using DataAccess.Interfaces;
using Newtonsoft.Json.Linq;

namespace DataAccess
{
    public class GenericRepository<TObject> : IRepository<TObject> where TObject : class
    {
        // used only in Insertion
        protected readonly IDbConnection _dbConnection;
        protected readonly ISystemTrackingLogger SystemTrackingLogger;
        
        public GenericRepository(IConnection dbConnection,ISystemTrackingLogger systemTrackingLogger)
        {
            _dbConnection = dbConnection.CreateConnection();
            SystemTrackingLogger = systemTrackingLogger;
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
                SystemTrackingLogger.LogErrorMessage(e.Message,e.StackTrace);
                throw new Exception("Insertion Failed");
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
                SystemTrackingLogger.LogErrorMessage(e.Message,e.StackTrace);
                throw new Exception("Selection From Table Failed");
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
                SystemTrackingLogger.LogErrorMessage(e.Message,e.StackTrace);
                throw new Exception("Update of Row Failed");
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
                SystemTrackingLogger.LogErrorMessage(e.Message,e.StackTrace);
                throw new Exception("Deletion Of Row Failed");
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
                SystemTrackingLogger.LogErrorMessage(e.Message, e.StackTrace);
                throw;
            }

            return isOk;
        }
    }
}