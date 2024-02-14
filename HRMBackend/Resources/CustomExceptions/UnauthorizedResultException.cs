namespace HRMBackend.Resources.CustomExceptions
{
    public class UnauthorizedResultException : Exception
    {
        #region Constructor
        public UnauthorizedResultException()
        {
        }

        public UnauthorizedResultException(string message)
            : base(message)
        {
        }

        public UnauthorizedResultException(string message, Exception inner)
            : base(message, inner)
        {
        }
        #endregion
    }
}
