namespace TechLibrary.Api.Domain.Entities;

public class Checkout : Entity
{
    public DateTime CheckoutDate { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime ExpectedReturnDate  {  get; set; }
    public DateTime? ReturnedDate {  get; set; }
}
