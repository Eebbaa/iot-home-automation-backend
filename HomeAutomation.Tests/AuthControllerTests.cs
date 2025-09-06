using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using iot_home_automation_backend.Controllers.API.Version1;
using iot_home_automation_backend.Models;
using iot_home_automation_backend.DTOs.Auth;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

public class AuthControllerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        // Mock UserManager
        var userStoreMock = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );

        // Mock SignInManager
        var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
        _signInManagerMock = new Mock<SignInManager<User>>(
            _userManagerMock.Object,
            contextAccessorMock.Object,
            userClaimsPrincipalFactoryMock.Object,
            null, null, null, null
        );

        _controller = new AuthController(_userManagerMock.Object, _signInManagerMock.Object);
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenUserIsCreated()
    {
        // Arrange
        var dto = new RegisterDto
        {
            FullName = "Test User",
            Email = "test@example.com",
            Password = "Password123!"
        };

        _userManagerMock
            .Setup(u => u.CreateAsync(It.IsAny<User>(), dto.Password))
            .ReturnsAsync(IdentityResult.Success);

        

        // Act
        var result = await _controller.Register(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.Equal("User registered successfully", response.Message);
    }

    [Fact]
   
    public async Task Login_ReturnsOk_WhenCredentialsAreValid()
    {
        // Arrange
        var dto = new LoginDto { Email = "test@example.com", Password = "Password123!" };

        _signInManagerMock
            .Setup(s => s.PasswordSignInAsync(dto.Email, dto.Password, false, false))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

        // Act
        var result = await _controller.Login(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.Equal("User logged in successfully", response.Message);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var dto = new LoginDto { Email = "wrong@example.com", Password = "wrongpass" };

        _signInManagerMock
            .Setup(s => s.PasswordSignInAsync(dto.Email, dto.Password, false, false))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

        // Act
        var result = await _controller.Login(dto);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(unauthorizedResult.Value);
        Assert.Equal("Invalid login attempt", response.Message);
    }
}
