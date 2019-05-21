using System;

namespace CanonicalEquation.Exceptions
{
    public class NotValidVariableArgumentException : ArgumentException
    {
        public NotValidVariableArgumentException()
        {

        }
        public NotValidVariableArgumentException(string message) : base(message)
        {

        }
        public NotValidVariableArgumentException(string message, string paramName) : base(message, paramName)
        {

        }
    }

    
}
