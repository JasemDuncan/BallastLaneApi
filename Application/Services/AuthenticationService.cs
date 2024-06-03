
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.ViewModels;
using ballastLaneApi.Domain.Entities;
using ballastLaneApi.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ballastLaneApi.Application.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<TokenViewModel> Authenticate(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new Exception("Username or password is incorrect");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenViewModel { Token = tokenString };
    }

    public async Task<UserViewModel> Register(UserViewModel user)
    {
        var existingUser = await _userRepository.GetUserByEmail(user.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        var salt = BCrypt.Net.BCrypt.GenerateSalt();

        var newUser = new User
        {
            UserName = user.UserName,
            LastName = user.LastName,
            Salt = salt,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt),
            Name = user.Name,
            Email = user.Email,
        };
        var createdUser = await _userRepository.CreateUser(newUser);

        return _mapper.Map<UserViewModel>(createdUser);
    }

    public async Task<UserViewModel> UpdateUser(UserViewModel user)
    {
        var existingUser = await _userRepository.GetUserById(user.UserId.Value) ?? throw new Exception("User not found");

        var updatedUser = new User
        {
            UserId = existingUser.UserId,
            UserName = user.UserName,
            LastName = user.LastName,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password, existingUser.Salt),
            Name = user.Name,
            Email = user.Email,
        };
        var updatedUserEntity = await _userRepository.UpdateUser(updatedUser);
        return _mapper.Map<UserViewModel>(updatedUserEntity);
    }
}
