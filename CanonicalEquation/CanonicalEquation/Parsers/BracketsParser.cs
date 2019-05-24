using System;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Extensions;
using CanonicalEquation.Helpers;

namespace CanonicalEquation.Parsers
{
    public static class BracketsParser
    {
        public static Brackets Parse(string bracketsString)
        {
            if(bracketsString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(bracketsString));

            if(!ParseHelper.IsBrackets(bracketsString)) throw new NotValidBracketArgumentException("Initial string does not contain brackets at the beginning and end");

            ParseHelper.ValidateBrackets(bracketsString);

            //Get sign ??
            
            var contentInsideBrackets = ParseHelper.GetContentForBrackets(bracketsString);

            var polynomial = PolynomialParser.Parse(contentInsideBrackets);

            return new Brackets(polynomial);
        }
    }
}