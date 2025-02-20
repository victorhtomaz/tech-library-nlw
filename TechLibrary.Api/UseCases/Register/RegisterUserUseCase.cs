using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.Data;
using TechLibrary.Api.Infraestructure.Security.Criptography;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.UseCases.Register;

public class RegisterUserUseCase
{
    private readonly TechLibraryDbContext _context;

    public RegisterUserUseCase(TechLibraryDbContext dbContext)
    {
        _context = dbContext;
    }

    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        Validate(request);

        var cryptography = new BCryptAlgorithm();

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = cryptography.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        var token = JwtTokenGenerator.Generate(user);

        return new ResponseRegisteredUserJson(user.Name, token);
    }

    private void Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existUser = _context.Users.Any(user => user.Email.Equals(request.Email));

        if (existUser)
            result.Errors.Add(new ValidationFailure("Email", "E-mail já registrado"));

        if (result.IsValid is false)
        {
            var erroMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(erroMessages);
        }
    }
}
