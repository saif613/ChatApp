namespace ChatApp.Application.Exceptions
{
    public class ConversationNotFoundException
        : Exception
    {
        public ConversationNotFoundException(Guid conversationId)
            : base(
                $"Conversation '{conversationId}' was not found.")
        {

        }

        public ConversationNotFoundException(
    string message)
    : base(message)
        {
        }
    }
}