namespace Aurora.Framework.Api
{
    public class ErrorDetailResponse
    {
        #region Private members

        private string _message;

        #endregion

        #region Class properties

        public int StatusCode { get; set; }

        public string? Message
        {
            get
            {
                return _message;
            }
        }

        public ErrorDetailCategory ErrorCategory { get; set; }

        public IList<ErrorMessage> Errors { get; }

        #endregion

        #region Constructors

        public ErrorDetailResponse(int statusCode, ErrorDetailCategory category)
        {
            StatusCode = statusCode;
            ErrorCategory = category;
            Errors = new List<ErrorMessage>();
            _message = string.Empty;
        }

        #endregion

        #region Public methods

        public void AddErrorMessage(string errorType, string message)
        {
            _message = string.IsNullOrWhiteSpace(Message)
                ? message
                : string.Format("{0}\n{1}", Message, message);

            Errors.Add(
                new ErrorMessage()
                {
                    ErrorType = errorType,
                    Message = message
                });
        }

        #endregion
    }
}