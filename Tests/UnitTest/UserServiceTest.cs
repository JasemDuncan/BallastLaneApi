using AutoMapper;
using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.Services;
using ballastLaneApi.Application.ViewModels;
using ballastLaneApi.Domain.Entities;
using ballastLaneApi.Infrastructure.Interfaces;
using ballastLaneApi.Mapping;
using Moq;
using NUnit.Framework;

namespace ballastLaneApi.Tests.UnitTest.UserServiceTest;
public class AuthenticationServiceTest
{
    private Mock<IUserRepository> _userRepository;
    private IMapper _mapper;
    private IConfiguration _configuration;
    private IAuthenticationService _authenticationService;
    private User _user;
    private List<User> _users;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        _authenticationService = new AuthenticationService(_userRepository.Object, _configuration, _mapper);
        _user = new User
        {
            UserName = "User 2",
            Email = "test@test.com",
            Password = BCrypt.Net.BCrypt.HashPassword("password"),
            LastName = "Test Last Name",
            Name = "TestName"
        };
        _users = new List<User>
        {
            new() {
                UserId = 1,
                UserName = "User 1",
                Email = "admin@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                LastName = "Last Name",
                Name = "Name"
            }
        };
    }

    [Test]
    public async Task Authenticate_WithValidCredentials_ReturnsToken()
    {
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((string email) => _users.FirstOrDefault(x => x.Email == email));

        var token = await _authenticationService.Authenticate("admin@test.com", "admin123");

        Assert.That(token, Is.Not.Null);
    }

    [Test]
    public void Authenticate_WithInvalidCredentials_ThrowsException()
    {
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((string email) => _users.FirstOrDefault(x => x.Email == email));

        Assert.ThrowsAsync<Exception>(() => _authenticationService.Authenticate("admin2@test.com", "admin123"));
    }

    [Test]
    public void Authenticate_WithInvalidPassword_ThrowsException()
    {
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((string email) => _users.FirstOrDefault(x => x.Email == email));

        Assert.ThrowsAsync<Exception>(() => _authenticationService.Authenticate("admin@test.com", "123"));
    }

    [Test]
    public async Task Register_WithValidUser_ReturnsUser()
    {
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((string email) => _users.FirstOrDefault(x => x.Email == email));
        _userRepository.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync((User user) => user);

        var user = await _authenticationService.Register(_mapper.Map<UserViewModel>(_user));

        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public void Register_WithExistingUser_ThrowsException()
    {
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((string email) => _users.FirstOrDefault(x => x.Email == email));

        Assert.ThrowsAsync<Exception>(() => _authenticationService.Register(_mapper.Map<UserViewModel>(_users.First())));
    }

    [Test]
    public void UpdateUser_WithInvalidUser_ThrowsException()
    {
        // Arrange
        // Setup
        _userRepository.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync((int userId) => _users.FirstOrDefault(x => x.UserId == userId));
        var id = 2;
        var userToUpdate = new UserViewModel
        {
            UserId = 2,
            UserName = "User 2",
            Email = "qwe@asd.com",
            Password = "password",
            LastName = "Test Last Name",
            Name = "TestName"
        };

        // Action, Assert
        Assert.ThrowsAsync<Exception>(() => _authenticationService.UpdateUser(id,userToUpdate));

    }
}