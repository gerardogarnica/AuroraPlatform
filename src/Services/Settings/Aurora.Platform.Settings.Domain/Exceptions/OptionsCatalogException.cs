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
            : base($"The options catalog '{code}' already exists and cannot be created again.") { }
    }

    public class InvalidOptionsIdentifierException : OptionsCatalogException
    {
        public InvalidOptionsIdentifierException(int optionsId)
            : base($"The options catalog ID '{optionsId}' does not exist.") { }
    }

    public class NonEditableOptionsCatalogException : OptionsCatalogException
    {
        public NonEditableOptionsCatalogException(string name)
            : base($"The options catalog '{name}' is not editable.") { }
    }
}