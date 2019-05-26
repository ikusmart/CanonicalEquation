using System;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Extensions;

namespace CanonicalEquation.Parsers
{
    public static class EquationParser 
    {
        public static Equation Parse(string equationString)
        {
            if (equationString.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(equationString), "String shouldn't be empty");
            var transformedEquationString = equationString.RemoveWhiteSpaces();

            var equationExpressionsStringParts = transformedEquationString.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (equationExpressionsStringParts.Length == 0)
                throw new NotValidEquationArgumentException("Symbol '=' not found in source string");
            if (equationExpressionsStringParts.Length == 1)
                throw new NotValidEquationArgumentException("The equation must be filled on either side of the symbol '='");
            if (equationExpressionsStringParts.Length > 2)
                throw new NotValidEquationArgumentException("There must be only one  symbol '=' in the equation");

            var leftPart = equationExpressionsStringParts[0];
            var rightPart = equationExpressionsStringParts[1];

            var left = PolynomialParser.Parse(leftPart);
            var right = PolynomialParser.Parse(rightPart);

            return new Equation(left, right);
        }

        public static ParseResult TryParse(string initialString, out Equation parsedValue)
        {
            var result = new ParseResult();
            try
            {
                parsedValue = Parse(initialString);
            }
            catch (Exception ex)
            {
                parsedValue = default(Equation);
                result.AddError(ex.Message);
            }

            return result;
        }
    }
}