using System;

namespace poprawa.Exceptions
{
    public class TeamMemberNotFoundException: Exception
    {
        public TeamMemberNotFoundException()
        {
        }
        
        public TeamMemberNotFoundException(string message) : base(message)
        {
        }
        
        public TeamMemberNotFoundException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}