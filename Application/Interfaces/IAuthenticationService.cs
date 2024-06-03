
using ballastLaneApi.Application.ViewModels;

namespace ballastLaneApi.Application.Interfaces;
public interface IAuthenticationService
{
    Task<TokenViewModel> Authenticate(string email, string password);
    Task<UserViewModel> Register(UserViewModel user);
    Task<UserViewModel> UpdateUser(UserViewModel user);
}
