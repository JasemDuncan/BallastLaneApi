using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ballastLaneApi.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<TokenViewModel> Login([FromBody] LoginViewModel user)
    {
        return await _authenticationService.Authenticate(user.Email, user.Password);
    }
}