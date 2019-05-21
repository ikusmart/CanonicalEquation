using System;

namespace CanonicalEquation.Exceptions
{
    public class NotValidPolynomialArgumentException : ArgumentException
    {
        public NotValidPolynomialArgumentException()
        {

        }
        public NotValidPolynomialArgumentException(string message) : base(message)
        {

        }
        public NotValidPolynomialArgumentException(string message, string paramName) : base(message, paramName)
        {

        }
    }
}