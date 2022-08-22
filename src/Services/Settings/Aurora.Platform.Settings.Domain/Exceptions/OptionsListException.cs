using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class OptionsListException : BusinessException
    {
        public OptionsListException(string message)
            : base("OptionsListException", message) { }
    }
}