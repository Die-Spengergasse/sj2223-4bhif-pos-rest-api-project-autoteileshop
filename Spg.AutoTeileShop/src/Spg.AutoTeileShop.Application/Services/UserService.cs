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
            if (user is not null)
                return _userRepository.SetUser(user);

            return null;
        }

        public User? Delete(User user)
        {
            if (_userRepository.GetById(user.Id) is not null)
            {
                return _userRepository.Delete(user);
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User? GetByGuid(Guid guid)
        {
            return _userRepository.GetByGuid(guid) ?? throw new Exception($"No User Found with Guid: {guid}");
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id) ?? throw new Exception($"No User Found with Id: {id}");
        }

        public User? Update(Guid guid, User user)
        {
            User user2 = GetByGuid(guid) ?? throw new Exception("no User found");
            if (user is not null)
            {
                user2.Addrese = user.Addrese;
                user2.Email = user.Email;
                user2.Nachname = user.Nachname;
                user2.Vorname = user.Vorname;
                user2.PW = user.PW;
                user2.Confirmed = user.Confirmed;
                user2.Role = user.Role;
                return _userRepository.UpdateUser(user2);
            }
            return null;
        }
    }
}
