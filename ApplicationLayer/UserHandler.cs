using ApplicationLayer.Dtos.Order_DTOs;
using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer
{
    public class UserHandler
    {

        private readonly IUserRepository<Users> _userRepository;

        public UserHandler(IUserRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }


        public GetUserDto? GetUser(string phoneNumber)
        {
            Users? user = _userRepository.RetrieveUser(phoneNumber);


            return new GetUserDto(){PhoneNumber = user.PhoneNumber, UserName = user.UserName, Id = user.Id};
        }


        public bool TryGetUserOrder(string phoneNumber,out RetrieveUserOrderDto? userOrderDto)
        {
            userOrderDto = null;
            if (!_userRepository.CheckUserExistByPhone(phoneNumber)) return false;

            Users user = _userRepository.RetrieveUser(phoneNumber)!;
            var userOrder = _userRepository.GetUserOrders(user).ToList();



            userOrderDto = new RetrieveUserOrderDto()
            {
                DeliveryManID = userOrder.ElementAt(0).DeliveryManId,
                DeliveryPrice = userOrder.ElementAt(0).DeliveryPrice,
                OrderDate = userOrder.ElementAt(0).OrderDate,
                Status = userOrder.ElementAt(0).OrderStatus.Status
            };


            foreach (var o in userOrder)
            {
                userOrderDto.Products.Add(o.Product);
                userOrderDto.TotalPrice += o.Product.Price * o.ProductAmount;
            }



            return true;
        }


        public bool TryAddNewOrders(string phoneNumber,List<AddOrderDto> retrieveUserOrder)
        {
            if (!_userRepository.CheckUserExistByPhone(phoneNumber)) return false;

            Users user = _userRepository.RetrieveUser(phoneNumber)!;
            List<Order> orders = new List<Order>();
            Order baseOrderInformation = new Order()
            {
                User = user,
                OrderDate = DateTime.Now,
                DeliveryPrice = 50,
            };  
                
            foreach (var order in retrieveUserOrder)
            {
                Order newOrder = baseOrderInformation;

                newOrder.Product = order.Product;
                newOrder.ProductAmount = order.Amount;
                orders.Add(newOrder);
            }

            return _userRepository.AddOrders(orders) != -1;
        }

    }
}
