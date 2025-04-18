﻿using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class GenericRepository<TObject> : 
        IExtendedRepository<TObject> where TObject : class
    {

        protected readonly ApplicationDbContext _dbContext;

        protected readonly DbSet<TObject?> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TObject?>();
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
                throw new Exception($"Failed to call function Add, Error {e.Message}, ErrorStack: {e.StackTrace}");
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
                throw new Exception($"Failed to call function GetById, Error {e.Message}, ErrorStack: {e.StackTrace}");
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
                throw new Exception($"Failed to call function Update, Error {e.Message}, ErrorStack: {e.StackTrace}");
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
                throw new Exception($"Failed to call function Delete, Error {e.Message}, ErrorStack: {e.StackTrace}");
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