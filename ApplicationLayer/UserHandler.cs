using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using DomainLayer.Interfaces;
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

    }
}
