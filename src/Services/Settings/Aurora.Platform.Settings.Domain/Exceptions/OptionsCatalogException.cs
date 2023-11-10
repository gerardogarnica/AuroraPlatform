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

    public class OptionCodeAlreadyExistsException : OptionsCatalogException
    {
        public OptionCodeAlreadyExistsException(string code)
            : base($"Option catalog '{code}' already exists and cannot be created again.") { }
    }

    public class InvalidOptionCodeException : OptionsCatalogException
    {
        public InvalidOptionCodeException(string code)
            : base($"Option catalog code '{code}' does not exist.") { }
    }

    public class InvalidOptionItemCodeException : OptionsCatalogException
    {
        public InvalidOptionItemCodeException(string optionCode, string itemCode)
            : base($"Item '{itemCode}' does not exist in option catalog '{optionCode}'.") { }
    }

    public class NonEditableOptionCatalogException : OptionsCatalogException
    {
        public NonEditableOptionCatalogException(string name)
            : base($"Option catalog '{name}' is not editable.") { }
    }
}