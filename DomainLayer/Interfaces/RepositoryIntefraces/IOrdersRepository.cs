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
        int AcceptUserOrders(User user, Delivery deliveryMan);

        int DeclineUserOrder(User user);

        int SetUserOrderAsDelivered(User user);
    }
}
