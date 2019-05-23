﻿using System;

namespace CanonicalEquation.Exceptions
{
    public class NotValidBracketArgumentException : ArgumentException
    {
        public NotValidBracketArgumentException()
        {

        }
        public NotValidBracketArgumentException(string message) : base(message)
        {

        }
        public NotValidBracketArgumentException(string message, string paramName) : base(message, paramName)
        {

        }
    }
}