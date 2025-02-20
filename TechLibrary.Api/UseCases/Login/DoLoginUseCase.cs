using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Infraestructure.Data;
using TechLibrary.Api.Infraestructure.Security.Criptography;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.UseCases.Login;

public class DoLoginUseCase
{
    private readonly TechLibraryDbContext _context;

    public DoLoginUseCase(TechLibraryDbContext context)
    {
        _context = context;
    }

    public ResponseRegisteredUserJson Execute(RequestLoginJson request)
    {
        var user = _context.Users.AsNoTracking().FirstOrDefault(user => user.Email.Equals(request.Email));
        if (user is null)
            throw new InvalidLoginException();

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.Verify(request.Password, user);

        if (passwordIsValid is false)
            throw new InvalidLoginException();

        var token = JwtTokenGenerator.Generate(user);

        return new ResponseRegisteredUserJson(user.Name, token);
    }
}
