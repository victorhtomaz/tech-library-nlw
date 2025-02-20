using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Login;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;

[Route("v1/login")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status500InternalServerError)]
    public IActionResult DoLogin(RequestLoginJson request, DoLoginUseCase useCase)
    {
        var response = useCase.Execute(request);

        return Ok(response);
    }
}
