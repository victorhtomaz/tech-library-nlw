using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.Data;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.UseCases.Checkouts;

public class RegisterBookCheckoutUseCase
{
    private readonly TechLibraryDbContext _context;
    private const int MAX_LOAN_DAYS = 7;
    public RegisterBookCheckoutUseCase(TechLibraryDbContext context)
    {
        _context = context;
    }

    public ResponseCheckoutJson Execute(Guid bookId, User user)
    {
        Validate(bookId);

        var checkout = new Checkout
        {
            UserId = user.Id,
            BookId = bookId,
            CheckoutDate = DateTime.UtcNow,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS),
        };

        _context.Checkouts.Add(checkout);
        _context.SaveChanges();

        return new ResponseCheckoutJson(checkout.CheckoutDate, checkout.ExpectedReturnDate, checkout.ReturnedDate);
       }
    private void Validate(Guid bookId)
    {
        var book = _context.Books.FirstOrDefault(book => book.Id == bookId);
        if (book is null)
            throw new NotFoundException("Livro não encontrado.");

        var amountBookNotReturned = _context.Checkouts
            .Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);

        if (amountBookNotReturned == book.Amount)
            throw new ConflictException("Livro não disponível para emprestimo.");
    }
}
