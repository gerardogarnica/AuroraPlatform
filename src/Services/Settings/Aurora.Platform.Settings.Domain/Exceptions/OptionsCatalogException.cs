using Aurora.Framework;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class OptionsCatalogException : BusinessException
    {
        public OptionsCatalogException(string message)
            : base("OptionsListException", message) { }
    }

    public class OptionsCatalogNullException : OptionsCatalogException
    {
        public OptionsCatalogNullException()
            : base("The record of the options catalog cannot be null.") { }
    }

    public class OptionsCodeAlreadyExistsException : OptionsCatalogException
    {
        public OptionsCodeAlreadyExistsException(string code)
            : base($"The options catalog code '{code}' already exists and cannot be created again.") { }
    }
}