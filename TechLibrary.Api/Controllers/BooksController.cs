using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Books.Filters;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;

[Route("v1/books")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet("filter")]
    [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
    public IActionResult Filter(int pageNumber, string? title, FilterBookUseCase useCase)
    {
        var request = new RequestFilterBookJson(pageNumber, title);

        var result = useCase.Execute(request);

        if (result.Pagination.TotalCount == 0)
            return NoContent();

        return Ok(result);
    }
}
