using System.Linq;

namespace Aurora.Framework.Exceptions
{
    public class ValidationException : Exception
    {
        #region Class properties

        public List<string> Errors { get; }

        #endregion

        #region Constructors

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        public ValidationException(IEnumerable<string> errors)
            : this()
        {
            Errors.AddRange(errors);
        }

        #endregion
    }
}