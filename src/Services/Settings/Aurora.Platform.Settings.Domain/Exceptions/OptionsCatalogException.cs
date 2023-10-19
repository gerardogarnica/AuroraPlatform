using Aurora.Framework;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class OptionsCatalogException : BusinessException
    {
        public OptionsCatalogException(string message)
            : base("OptionsListException", message) { }
    }

    public class OptionsListNullException : OptionsCatalogException
    {
        public OptionsListNullException()
            : base("The record of the options list cannot be null.") { }
    }

    public class ExistsOptionsListCodeException : OptionsCatalogException
    {
        public ExistsOptionsListCodeException(string code)
            : base($"The options list code {code} already exists and cannot be created again.") { }
    }
}