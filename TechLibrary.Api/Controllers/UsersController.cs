using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
 
namespace TechLibrary.Api.Controllers;

[Route("v1/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status500InternalServerError)]
    public IActionResult Register(RequestUserJson request, RegisterUserUseCase useCase)
    {
        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}