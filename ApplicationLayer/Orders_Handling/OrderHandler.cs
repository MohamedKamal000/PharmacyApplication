
using ApplicationLayer.Dtos.Delivery_DTOs;
using ApplicationLayer.Dtos.Order_DTOs;
using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Orders_Handling
{
    public class OrderHandler
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly IDeliveryRepository _deliveryRepository;
         

        public OrderHandler(IOrdersRepository ordersRepository, IUserRepository<User> userRepository, 
            IDeliveryRepository deliveryRepository)
        {

            _ordersRepository = ordersRepository;
            _userRepository = userRepository;
            _deliveryRepository = deliveryRepository;
        }



        public bool TryGetAllOrders(out List<GetOrderDto> orders)
        {
            List<Order> O = _ordersRepository.GetAll().ToList();
            orders = new List<GetOrderDto>();
            if (O.Count <= 0) return false;


            foreach (var order in O)
            {
                orders.Add(
                    new GetOrderDto(){Status = order.OrderStatus.Status,
                    User = new GetUserDto()
                    {
                        UserName = order.User.UserName,
                        PhoneNumber = order.User.PhoneNumber,
                        Id = order.UserId
                    }});
            }

            return true;
        }



        public bool TryGetAllOrdersWithStatus(OrderStatusEnum orderStatus,out List<GetOrderDto> orders)
        {
            List<Order> O = _ordersRepository.GetAllWithStatus(orderStatus).ToList();
            orders = new List<GetOrderDto>();
            if (O.Count <= 0) return false;


            foreach (var order in O)
            {
                orders.Add(
                    new GetOrderDto()
                    {
                        Status = order.OrderStatus.Status,
                        User = new GetUserDto()
                        {
                            UserName = order.User.UserName,
                            PhoneNumber = order.User.PhoneNumber,
                            Id = order.UserId
                        }
                    });
            }

            return true;
        }


        public bool TryAcceptOrder(string userPhoneNumber, string deliveryPhoneNumber)
        {
            User ? user = _userRepository.RetrieveUser(userPhoneNumber);
            Delivery? deliveryMan = _deliveryRepository.SearchDeliveryManByPhone(deliveryPhoneNumber);
            if (user == null || deliveryMan == null) return false;


            return _ordersRepository.AcceptUserOrders(user, deliveryMan) != -1;
        }

        public bool TryDeclineUserOrder(string userPhoneNumber)
        {
            User? user = _userRepository.RetrieveUser(userPhoneNumber);
            // should retrive delivery man here as well
            if (user == null) return false;


            return _ordersRepository.DeclineUserOrder(user) != -1;
        }


        public bool TrySetOrderAsDelivered(string userPhoneNumber)
        {
            User? user = _userRepository.RetrieveUser(userPhoneNumber);
            // should retrive delivery man here as well
            if (user == null) return false;


            return _ordersRepository.SetUserOrderAsDelivered(user) != -1;

        }


        public bool TryDeleteOrder(int orderId)
        {
            Order? order = _ordersRepository.GetById(orderId);
            if (order == null) return false;

            return _ordersRepository.Delete(order) != -1;
        }
    }
}
