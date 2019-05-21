using System;
using System.Collections.Generic;
using System.Text;

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
