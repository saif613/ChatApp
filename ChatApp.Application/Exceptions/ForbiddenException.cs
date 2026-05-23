namespace ChatApp.Application.Exceptions
{
    public class ForbiddenException
        : Exception
    {
        public ForbiddenException()
            : base("Access denied.")
        {

        }
    }
}