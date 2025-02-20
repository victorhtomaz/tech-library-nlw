using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Api.UseCases.Checkouts;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;

[Route("v1/checkouts")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{
    private readonly LoggedUserService _loggedUserService;
    public CheckoutsController(LoggedUserService loggedUserService)
    {
        _loggedUserService = loggedUserService;
    }

    [HttpPost]
    [Route("{bookId:guid}")]
    [ProducesResponseType(typeof(ResponseCheckoutJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status500InternalServerError)]
    public IActionResult BookCheckout(Guid bookId, RegisterBookCheckoutUseCase useCase)
    {
        var user = _loggedUserService.GetUser();

        var result = useCase.Execute(bookId, user);

        return Created(string.Empty, result);
    }

    [HttpPatch]
    [Route("{bookId:guid}")]
    [ProducesResponseType(typeof(ResponseCheckoutJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult ReturnBookCheckout(Guid bookId, ReturnBookCheckoutUseCase useCase)
    {
        var user = _loggedUserService.GetUser();

        var result = useCase.Execute(bookId, user);

        return Ok(result);
    }
}