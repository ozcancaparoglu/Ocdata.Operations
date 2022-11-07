namespace Ocdata.Operations.Helpers.ExceptionHelper
{
    public abstract class BadRequestException : ApplicationException
    {
        protected BadRequestException(string message)
            : base("Bad Request", message)
        {
        }
    }
}
