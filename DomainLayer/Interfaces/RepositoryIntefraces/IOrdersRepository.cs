using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        ICollection<Order> GetAllWithStatus(OrderStatusEnum status);
        int AcceptUserOrders(Users user, Delivery deliveryMan);

        int DeclineUserOrder(Users user);

        int SetUserOrderAsDelivered(Users user);
    }
}
