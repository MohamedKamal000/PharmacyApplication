using ApplicationLayer.Dtos.Order_DTOs;
using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Users_Handling
{
    public class UserHandler
    {

        private readonly IUserRepository<Users> _userRepository;
        private readonly IProductRepository _productRepository;

        public UserHandler(IUserRepository<Users> userRepository,IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }


        public GetUserDto? GetUser(string phoneNumber)
        {
            Users? user = _userRepository.RetrieveUser(phoneNumber);


            return new GetUserDto(){PhoneNumber = user.PhoneNumber, UserName = user.UserName, Id = user.Id};
        }


        public bool TryGetUserOrder(string phoneNumber,out RetrieveUserOrderDetailsDto? userOrderDto)
        {
            userOrderDto = null;
            if (!_userRepository.CheckUserExistByPhone(phoneNumber)) return false;

            Users user = _userRepository.RetrieveUser(phoneNumber)!;
            var userOrder = _userRepository.GetUserOrders(user).ToList();



            userOrderDto = new RetrieveUserOrderDetailsDto()
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
                if (!CanUserOrderThis(order.ProductName,order.Amount)) return false;

                Order newOrder = baseOrderInformation;
                newOrder.Product = _productRepository.GetProductByName(order.ProductName)!;
                newOrder.ProductAmount = order.Amount;
                orders.Add(newOrder);
            }

            return _userRepository.AddOrders(orders) != -1;
        }


        private bool CanUserOrderThis(string productName,int amount)
        {
            Product? product = _productRepository.GetProductByName(productName);
            if (product == null) return false;


            return product.Stock >= amount;
        }
    }
}
