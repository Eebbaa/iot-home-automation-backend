using iot_home_automation_backend.Controllers.API.Version1;
using iot_home_automation_backend.DTOs.Auth;
using iot_home_automation_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

public class AuthControllerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly Mock<IConfiguration> _configurationMock;
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
            //Mock.Of<IHttpContextAccessor>(),    
            contextAccessorMock.Object,
            userClaimsPrincipalFactoryMock.Object,
            null, null, null, null
        );
       // Mock IConfiguration
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(x => x["Jwt:Key"]).Returns("ThisIsASecretKeyForJwtDontUseInProd123!");
        _configurationMock.Setup(x => x["Jwt:Issuer"]).Returns("iot-home-automation-api");

        // Pass mock into controller
        _controller = new AuthController(
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _configurationMock.Object
        );
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
            .ReturnsAsync(SignInResult.Success);

        // Mock UserManager to return a valid user (needed for GenerateJwtToken)
        _userManagerMock
           .Setup(u => u.FindByEmailAsync(dto.Email))
           .ReturnsAsync(new User
           {
                 Id = Guid.NewGuid(),
                 Email = dto.Email,
                 UserName = dto.Email,
                 FullName = "Test User"
           });

        // Act
        var result = await _controller.Login(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.Equal("User logged in successfully", response.Message);
        Assert.False(string.IsNullOrEmpty(response.Token)); //extra check for JWT
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var dto = new LoginDto { Email = "wrong@example.com", Password = "wrongpass" };

        _signInManagerMock
            .Setup(s => s.PasswordSignInAsync(dto.Email, dto.Password, false, false))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _controller.Login(dto);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(unauthorizedResult.Value);
        Assert.Equal("Invalid login attempt", response.Message);
    }

    //ForgotPassword
    [Fact]
    public async Task ForgotPassword_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var dto = new ForgotPasswordDto { Email = "test@example.com" };
        var user = new User { Email = dto.Email };

        _userManagerMock
            .Setup(u => u.FindByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(u => u.GeneratePasswordResetTokenAsync(user))
            .ReturnsAsync("fake-token");

        // Act
        var result = await _controller.ForgotPassword(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.Equal("Password reset token generated successfully.", response.Message);
        Assert.Equal("fake-token", response.Token);
    }

    [Fact]
    public async Task ForgotPassword_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var dto = new ForgotPasswordDto { Email = "notfound@example.com" };

        _userManagerMock
            .Setup(u => u.FindByEmailAsync(dto.Email))
            .ReturnsAsync((User)null);

        // Act
        var result = await _controller.ForgotPassword(dto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(notFoundResult.Value);
        Assert.Equal("User with this email does not exist.", response.Message);
    }

    [Fact]
    public async Task ResetPassword_ReturnsOk_WhenPasswordResetSucceeds()
    {
        // Arrange
        var dto = new ResetPasswordDto
        {
            Email = "test@example.com",
            Token = "valid-token",
            NewPassword = "NewPassword123!"
        };

        var user = new User { Email = dto.Email };

        _userManagerMock
            .Setup(u => u.FindByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(u => u.ResetPasswordAsync(user, dto.Token, dto.NewPassword))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _controller.ResetPassword(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.Equal("Password reset successfully.", response.Message);
    }

    [Fact]
    public async Task ResetPassword_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var dto = new ResetPasswordDto
        {
            Email = "notfound@example.com",
            Token = "some-token",
            NewPassword = "NewPassword123!"
        };

        _userManagerMock
            .Setup(u => u.FindByEmailAsync(dto.Email))
            .ReturnsAsync((User)null);

        // Act
        var result = await _controller.ResetPassword(dto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(notFoundResult.Value);
        Assert.Equal("User not found.", response.Message);
    }

    [Fact]
    public async Task ResetPassword_ReturnsBadRequest_WhenPasswordResetFails()
    {
        // Arrange
        var dto = new ResetPasswordDto
        {
            Email = "test@example.com",
            Token = "invalid-token",
            NewPassword = "NewPassword123!"
        };

        var user = new User { Email = dto.Email };

        _userManagerMock
            .Setup(u => u.FindByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(u => u.ResetPasswordAsync(user, dto.Token, dto.NewPassword))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

        // Act
        var result = await _controller.ResetPassword(dto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<AuthResponseDto>(badRequestResult.Value);
        Assert.Equal("Password reset failed.", response.Message);
        Assert.Contains("Invalid token", response.Errors);
    }
}
