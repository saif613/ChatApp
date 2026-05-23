namespace ChatApp.Application.Exceptions
{
    public class MessageNotFoundException
        : Exception
    {
        public MessageNotFoundException(Guid messageId)
            : base(
                $"Message with id '{messageId}' was not found.")
        {

        }
    }
}