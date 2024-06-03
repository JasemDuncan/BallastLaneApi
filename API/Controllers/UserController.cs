
using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BallastLaneApplications.ballastLane.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public UserController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPut("{id}")]
    public async Task<UserViewModel> UpdateUser(int id,UserViewModel user)
    {
        return await _authenticationService.UpdateUser(id,user);
    }

    [HttpPost]
    public async Task<UserViewModel> Register(UserViewModel user)
    {
        return await _authenticationService.Register(user);
    }
}
