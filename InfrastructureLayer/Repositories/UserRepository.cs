﻿using System.Collections.Specialized;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository<Users>
    {
        
        public UserRepository(ApplicationDbContext connection) 
            : base(connection)
        {
            
        }

        public ICollection<Order> GetUserOrders(Users user)
        {
            List<Order> userOrder = new List<Order>();

            try
            {
                using (_dbContext)
                {
                    _dbContext.Entry(user).Collection(u => u.Orders).Load();

                    foreach (var o in user.Orders)
                    {
                        _dbContext.Entry(o).Reference(o => o.Product);
                        _dbContext.Entry(o).Reference(o => o.DeliveryMan);
                        _dbContext.Entry(o).Reference(o => o.OrderStatus);
                        _dbContext.Entry(o).Reference(o => o.User);
                        o.User.Password = "";
                    }
                }

                userOrder = user.Orders.ToList();
            }

            catch (Exception e)
            {
                throw new Exception($"Couldn't get user Orders, Error Message: {e.Message}");
            }


            return userOrder;
        }

        public  Users? RetrieveUserCredentials(string phoneNumber)
        {
            Users user;

            try
            {
                user = _dbSet.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            }
            catch (Exception e)
            {
                throw new Exception($"GetUser Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }
                
            return user;
        }

        public int AddOrders(List<Order> orders)
        {
            int result = -1;
            try
            {
                using (_dbContext)
                {
                    foreach (var o in orders)
                    {
                        _dbContext.Orders.Add(o);
                    }

                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"AddOrders Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return result;
        }


        public bool CheckUserExistByPhone(string phoneNumber)
        {
            bool found = false;
            try
            {
                found = _dbSet.FirstOrDefault(u => u.PhoneNumber == phoneNumber) != null;
            }
            catch(Exception e)
            {
                throw new Exception($"CheckUserExistByPhone Failed, error Message{e.Message}" +
                                    $"Error Stack: {e.StackTrace}");
            }

            return found;
        }
    }
}