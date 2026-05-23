namespace ChatApp.Application.Exceptions
{
    public class UnauthorizedMessageAccessException
        : Exception
    {
        public UnauthorizedMessageAccessException()
            : base(
                "You are not authorized to access this message.")
        {

        }
    }
}