namespace Ocdata.Operations.Helpers.ExceptionHelper
{
    public abstract class NotFoundException : ApplicationException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }
}
