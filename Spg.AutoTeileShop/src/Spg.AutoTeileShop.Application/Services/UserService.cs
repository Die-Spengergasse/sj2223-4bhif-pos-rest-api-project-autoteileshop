using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Application.Services
{
    public class UserService : IAddUpdateableUserService, IDeletableUserService, IReadOnlyUserService
    {
        protected readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Add(User user)
        {
            if(user is not null)
            return _userRepository.SetUser(user);
            
            return null;
        }

        public User? Delet(User user)
        {
            if (_userRepository.GetById(user.Id) is not null)
            {
                return _userRepository.Delet(user);
            }
            return user;
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id) ?? throw new Exception($"No User Found with Id: {id}");
        }

        public User? Update(User user)
        {
            if(user is not null)
            return _userRepository.UpdateUser(user);
            return null;
        }
    }    
}
