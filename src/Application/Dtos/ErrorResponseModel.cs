namespace Application.Dtos
{
    public record ErrorResponseModel(
        int Code,
        string Message,
        Dictionary<string, object>? Details = null
        )
    {
    }
}
