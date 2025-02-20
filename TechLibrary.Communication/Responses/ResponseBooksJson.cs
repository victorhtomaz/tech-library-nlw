namespace TechLibrary.Communication.Responses;

public record ResponseBooksJson(ResponsePaginationJson Pagination, List<ResponseBookJson> Books);