namespace Aurora.Framework
{
    public class PlatformException : Exception
    {
        public PlatformException()
            : base() { }

        public PlatformException(string message)
            : base(message) { }

        public PlatformException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}