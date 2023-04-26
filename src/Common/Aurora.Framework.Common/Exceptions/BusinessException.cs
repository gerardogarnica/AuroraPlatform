namespace Aurora.Framework
{
    public class BusinessException : Exception
    {
        #region Private members

        private readonly string _errorType;

        #endregion

        #region Class properties

        public string ErrorType
        {
            get { return _errorType; }
        }

        #endregion

        #region Constructors

        public BusinessException()
            : base()
        {
            _errorType = string.Empty;
        }

        public BusinessException(string errorType, string message)
            : base(message)
        {
            _errorType = errorType;
        }

        public BusinessException(string errorType, string message, Exception exception)
            : base(message, exception)
        {
            _errorType = errorType;
            Source = exception.Source;
        }

        #endregion
    }
}