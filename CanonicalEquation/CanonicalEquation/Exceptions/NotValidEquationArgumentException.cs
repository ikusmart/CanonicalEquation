using System;

namespace CanonicalEquation.Lib.Exceptions
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