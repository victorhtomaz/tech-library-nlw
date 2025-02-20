using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.Data;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.UseCases.Checkouts;

public class ReturnBookCheckoutUseCase
{
    private readonly TechLibraryDbContext _context;

    public ReturnBookCheckoutUseCase(TechLibraryDbContext context)
    {
        _context = context;
    }

    public ResponseCheckoutJson Execute(Guid bookId, User user)
    {
        var checkout = GetCheckout(bookId, user);

        checkout.ReturnedDate = DateTime.UtcNow;
        
        _context.Checkouts.Update(checkout);
        _context.SaveChanges();

        return new ResponseCheckoutJson(checkout.CheckoutDate, checkout.ExpectedReturnDate, checkout.ReturnedDate);
    }

    private Checkout GetCheckout(Guid bookId, User user)
    {
        var checkout = _context.Checkouts
            .FirstOrDefault(checkout => checkout.BookId == bookId && checkout.UserId == user.Id);

        if (checkout is null)
            throw new NotFoundException("Emprestimo não encontrado.");

        return checkout;
    }
}