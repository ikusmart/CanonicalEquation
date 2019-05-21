using System;

namespace CanonicalEquation.Exceptions
{
    public class NotValidEquationArgumentException : ArgumentException
    {
        public NotValidEquationArgumentException()
        {

        }
        public NotValidEquationArgumentException(string message) : base(message)
        {

        }
        public NotValidEquationArgumentException(string message, string paramName) : base(message, paramName)
        {
            
        }
    }
}