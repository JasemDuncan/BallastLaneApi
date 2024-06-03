
using ballastLaneApi.Domain.Entities;

namespace ballastLaneApi.Infrastructure.Interfaces;
public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
}
