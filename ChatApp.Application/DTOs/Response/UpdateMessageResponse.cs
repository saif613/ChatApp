namespace ChatApp.Application.DTOs.Response
{
    public record UpdateMessageResponse(
        Guid Id,
        string Content,
        DateTime? EditedAt
    );
}