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
            if (!_userRepository.CheckUserExistByPhone(phoneNumber)) return false;

            var userOrder = _userRepository.GetUserOrders(new Users() { PhoneNumber = phoneNumber }).ToList();



            userOrderDto = new UserOrderDto()
            {
                User = new GetUserDto()
                {
                    Id = userOrder.ElementAt(0).User.Id,
                    PhoneNumber = phoneNumber,
                    UserName = userOrder.ElementAt(0).User.UserName
                },

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

    }
}
