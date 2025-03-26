
using ApplicationLayer.Dtos.Order_DTOs;
using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Orders_Handling
{
    public class OrderHandler
    {

        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserRepository<Users> _userRepository;

        // delete order required 

        public OrderHandler(IOrdersRepository ordersRepository, IUserRepository<Users> userRepository)
        {

            _ordersRepository = ordersRepository;
            _userRepository = userRepository;
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


        public bool TryAcceptOrder(GetUserDto userDto, Delivery deliveryMan)
        {
            Users ? user = _userRepository.RetrieveUser(userDto.PhoneNumber);
            // should retrive delivery man here as well
            if (user == null) return false;


            return _ordersRepository.AcceptUserOrders(user, deliveryMan) != -1;
        }

        public bool TryDeclineUserOrder(GetUserDto userDto)
        {
            Users? user = _userRepository.RetrieveUser(userDto.PhoneNumber);
            // should retrive delivery man here as well
            if (user == null) return false;


            return _ordersRepository.DeclineUserOrder(user) != -1;
        }


        public bool TrySetOrderAsDelivered(GetUserDto userDto)
        {
            Users? user = _userRepository.RetrieveUser(userDto.PhoneNumber);
            // should retrive delivery man here as well
            if (user == null) return false;


            return _ordersRepository.SetUserOrderAsDelivered(user) != -1;

        }

    }
}
