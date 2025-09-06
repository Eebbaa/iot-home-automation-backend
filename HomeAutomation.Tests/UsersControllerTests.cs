using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iot_home_automation_backend.Controllers.API.Version1;
using iot_home_automation_backend.Data;
using iot_home_automation_backend.DTOs;
using iot_home_automation_backend.Models;
using iot_home_automation_backend.DTOs.User;


public class UsersControllerTests
{

    private readonly ApplicationDbContext _context;
    private readonly UserController _controller;

    public UsersControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "UserControllerTestDb")
              .Options;

        _context = new ApplicationDbContext(options);

        // Seed test data
        _context.Users.Add(new iot_home_automation_backend.Models.User
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "John Doe",
            Email = "john@example.com",
            CreatedAt = DateTime.UtcNow
        });
        _context.SaveChanges();

        _controller = new UserController(_context);
    }

    [Fact]
    public async Task GetUsers_ReturnsOk_WhenUsersExist()
    {
        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var users = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task GetUser_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var user = _context.Users.First();

        // Act
        var result = await _controller.GetUser(user.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(user.Email, dto.Email);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Act
        var result = await _controller.GetUser(Guid.NewGuid().ToString());

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Contains("not found", notFound.Value.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task UpdateUser_ReturnsOk_WhenUserIsUpdated()
    {
        // Arrange
        var user = _context.Users.First();
        var dto = new UpdateUserDto
        {
            FullName = "Updated Name",
            Email = "updated@example.com"
        };

        // Act
        var result = await _controller.UpdateUser(user.Id, dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Contains("updated successfully", okResult.Value.ToString(), StringComparison.OrdinalIgnoreCase);

        var updatedUser = _context.Users.Find(user.Id);
        Assert.Equal("Updated Name", updatedUser.FullName);
    }

    [Fact]
    public async Task PatchUser_UpdatesOnlyProvidedFields()
    {
        // Arrange
        var user = _context.Users.First();
        var dto = new PatchUserDto
        {
            FullName = "Patched Name" // Only update FullName
        };

        // Act
        var result = await _controller.PatchUser(user.Id, dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedUser = _context.Users.Find(user.Id);
        Assert.Equal("Patched Name", updatedUser.FullName);
        Assert.Equal(user.Email, updatedUser.Email); // Email unchanged
    }

    [Fact]
    public async Task DeleteUser_RemovesUser_WhenExists()
    {
        // Arrange
        var user = _context.Users.First();

        // Act
        var result = await _controller.DeleteUser(user.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Contains("deleted successfully", okResult.Value.ToString(), StringComparison.OrdinalIgnoreCase);

        var deletedUser = await _context.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }

}



