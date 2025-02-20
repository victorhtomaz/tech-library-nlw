using System.Security.Claims;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.Data;

namespace TechLibrary.Api.Services.LoggedUser;

public class LoggedUserService
{
    public readonly IHttpContextAccessor _httpContextAccessor;
    public readonly TechLibraryDbContext _dbContext;

    public LoggedUserService(IHttpContextAccessor httpContextAccessor, TechLibraryDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public User GetUser()
    {
        var loggedInUser = _httpContextAccessor.HttpContext?.User;
        var userId = loggedInUser?.FindFirstValue("Id") ?? string.Empty;

        var user = _dbContext.Users.FirstOrDefault(user => user.Id == Guid.Parse(userId));

        return user;
    }
}