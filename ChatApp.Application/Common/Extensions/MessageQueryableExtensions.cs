using ChatApp.Domain.Entities;

public static class MessageQueryableExtensions
{
    public static IQueryable<Message> VisibleToUser(this IQueryable<Message> query, string userId)
    {
        return query.Where(m => !((m.SenderId == userId && m.IsDeletedBySender) || (m.ReceiverId == userId && m.IsDeletedByReceiver)));
    }
}