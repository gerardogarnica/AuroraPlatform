using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class OptionsListException : BusinessException
    {
        protected const string OptionsListNullMessage = "El registro de lista de opciones no puede ser nulo.";

        public OptionsListException(string message)
            : base("OptionsListException", message) { }
    }

    public class OptionsListNullException : OptionsListException
    {
        public OptionsListNullException()
            : base(OptionsListNullMessage) { }
    }
}