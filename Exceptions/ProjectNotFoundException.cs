using System;

namespace poprawa.Exceptions
{
    public class ProjectNotFoundExceptionException: Exception
    {
        public ProjectNotFoundExceptionException()
        {
        }
        
        public ProjectNotFoundExceptionException(string message) : base(message)
        {
        }
        
        public ProjectNotFoundExceptionException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}