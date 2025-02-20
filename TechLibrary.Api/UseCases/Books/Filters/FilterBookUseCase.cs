using TechLibrary.Api.Infraestructure.Data;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Books.Filters;

public class FilterBookUseCase
{
    private readonly TechLibraryDbContext _context;
    private const int PAGE_SIZE = 10;

    public FilterBookUseCase(TechLibraryDbContext context)
    {
        _context = context;
    }

    public ResponseBooksJson Execute(RequestFilterBookJson request)
    {
        var skip = (request.PageNumber - 1) * PAGE_SIZE;

        var query = _context.Books.AsQueryable();

        if (string.IsNullOrWhiteSpace(request.Title) is false)
            query = query.Where(book => book.Title.ToLower().Contains(request.Title.ToLower()));

        query = query.OrderBy(book => book.Title)
            .ThenBy(book => book.Author)
            .Skip(skip)
            .Take(PAGE_SIZE);   

        var totalCount = 0;

        if (string.IsNullOrWhiteSpace(request.Title))
            totalCount = _context.Books.Count();
        else
            totalCount = _context.Books.Count(book => book.Title.ToLower().Contains(request.Title.ToLower()));

        var paginationResponse = new ResponsePaginationJson(request.PageNumber, totalCount);

        var books = query
            .Select(book => new ResponseBookJson(book.Id, book.Title, book.Author))
            .ToList();

        return new ResponseBooksJson(paginationResponse, books);
    }
}
