namespace TechLibrary.Communication.Responses;

public record ResponseCheckoutJson(DateTime CheckoutDate, DateTime ExpectedReturnDate, DateTime? ReturnedDate);