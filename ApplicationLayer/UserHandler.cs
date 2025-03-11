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


        public Users? GetUser(string phoneNumber)
        {
            Users? user = _userRepository.RetrieveUserCredentials(phoneNumber);

            return user;
        }


        public bool GetUserOrder(string phoneNumber,out UserOrderDto? userOrderDto)
        {
            userOrderDto = null;
            if (!_userRepository.CheckExist(new KeyValuePair<string, object>("PhoneNumber", phoneNumber))) return false;

            var userOrder = _userRepository.GetUserOrders(new Users() { PhoneNumber = phoneNumber });


            userOrderDto = new UserOrderDto()
            {
                User = new GetUserDto()
                {
                    UserName = userOrder.User.UserName,
                    Id = userOrder.User.Id, PhoneNumber = userOrder.User.PhoneNumber
                },
                Products = userOrder.Products,
                DeliveryManID = userOrder.DeliveryManID,
                DeliveryPrice = userOrder.DeliveryPrice,
                OrderDate = userOrder.OrderDate,
                Status = userOrder.Status,
                TotalPrice = userOrder.TotalPrice
            };

            return true;
        }
    }
}
