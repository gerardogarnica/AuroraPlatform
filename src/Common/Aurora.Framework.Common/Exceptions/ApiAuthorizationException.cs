namespace Aurora.Framework
{
    public class ApiAuthorizationException : Exception
    {
        public ApiAuthorizationException()
            : base("There is no authorization for this operation.") { }
    }
}