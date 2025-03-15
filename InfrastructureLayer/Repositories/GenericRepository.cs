using System.Data;
using System.Reflection;
using Dapper;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace InfrastructureLayer.Repositories
{
    public class GenericRepository<TObject> : 
        IExtendedRepository<TObject> where TObject : class
    {
        // used only in Insertion
        protected readonly IDbConnection _dbConnection;

        protected readonly ApplicationDbContext _dbContext;

        protected readonly DbSet<TObject?> _dbSet;
        
        public GenericRepository(IConnection dbConnection)
        {
            _dbConnection = dbConnection.CreateConnection();
        }

        public GenericRepository(ApplicationDbContext dbContext, DbSet<TObject?> set)
        {
            _dbContext = dbContext;
            _dbSet = set;
        }
        
        public int Add(TObject? rowInserted)
        {
            int resultID = -1;
            
            try
            {
                _dbSet.Add(rowInserted);
                resultID = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Insertion Failed, error Message{e.Message}" +
                                   $"Error Stack: {e.StackTrace}");
            }
            

            
            return resultID;
        }
        

        public  TObject? GetById(int id)
        {
            TObject? obj;
            try
            {
                obj = _dbSet.Find(id);
            }
            catch (Exception e)
            {
                throw new Exception($"Getting Element Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }


            return obj;
        }

        public  int Update(TObject rowsUpdated)
        {
            int result = -1;
            try
            {
                _dbSet.Attach(rowsUpdated);
                _dbSet.Entry(rowsUpdated).State = EntityState.Modified;
                result = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Update Failed, error Message{e.Message}"
                                    + $"Error Stack: {e.StackTrace}");
            }

            return result;
        }

        public  int Delete(TObject deletedObj)
        {
            int result = -1;

            try
            {
                _dbSet.Remove(deletedObj);
                result = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Deleting Failed, error Message{e.Message}"
                                    + $"Error Stack: {e.StackTrace}");
            }

            return result;
        }


        public bool CheckExist(int id)
        {
            return _dbSet.Find(id) != null;
        }

        public virtual IEnumerable<TObject> GetAll()
        {
            return _dbSet.ToList()!;
        }
    }
}