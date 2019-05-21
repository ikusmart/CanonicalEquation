using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Extensions;

namespace CanonicalEquation
{
    public class Polynomial
    {
        private static readonly string AllowedSymbols = " .+*-()^";
        private readonly string _initialString;

        public IEnumerable<Monomial> Monomials { get; } = new List<Monomial>();

        public Polynomial(string initialString)
        {
            _initialString = initialString;
        }

        public static Polynomial Parse(string polynomialString)
        {
            if(polynomialString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(polynomialString), "Initial polynomial string must be not empty");

            if (!IsContainsOnlyAllowedSymbols(polynomialString))
            {
                throw new NotValidPolynomialArgumentException(
                    $"Initial polynomial string must contains only letters, digits or special symbols: '{AllowedSymbols}'", nameof(polynomialString));
            }

            ValidateBrackets(polynomialString);

            string simplePolynomialString = IgnoreSpaces(polynomialString);
            var monomialsParts = GetMonomials(simplePolynomialString);

            return new Polynomial(polynomialString);
        }

        private static string IgnoreSpaces(string input)
        {
            return new string(input.Where(c => c != ' ').ToArray());
        }

        private static IEnumerable<string> GetMonomials(string polynomialString)
        {
            var result = new List<string>();
            var monomialItem = new StringBuilder(polynomialString.Length);

            bool isOpenBracket = false;
            for (int i = 0; i < polynomialString.Length; i++)
            {
                var currentSymbol = polynomialString[i];
                if (currentSymbol == '(')
                {
                    var j = FindClosingBracket(polynomialString, i);
                    for (int k = i; k < j+1; k++)
                    {
                        monomialItem.Append(polynomialString[k]);
                    }
                }
                else if (currentSymbol == '+' || currentSymbol == '-' || i == polynomialString.Length-1)
                {
                    if (monomialItem.Length > 0)
                    {
                        result.Add(monomialItem.ToString());
                        result.Clear();
                    }

                    monomialItem.Append(currentSymbol);
                }
                // + or - -> new monomial
                // ' ' ignore
                // '(' - initial search of closing brackets '('
                // '.*^', letters or numbers - part of monomial
            }
            return result;

        }

        private static int FindClosingBracket(string stringValue, int start)
        {
            var bracketsStack = new Stack<int>();
            int i = start;

            do
            {
                if (stringValue[i] == '(')
                {
                    bracketsStack.Push(i);
                }
                else if (stringValue[i] == ')')
                {
                    bracketsStack.Pop();
                }

                i++;
            } while (bracketsStack.Count > 0 && i < stringValue.Length);

            if (bracketsStack.Count == 0)
            {
                return i - 1;
            }

            // TODO: position in original input is different than in compact, oops
            throw new FormatException($"Bracket '(' at zero-based position {start} is missing its counterpart ')'.");
        }

        private static void ValidateBrackets(string polynomialString)
        {
            var calculateBracketsResult = CalculateBrackets(polynomialString);

            if(calculateBracketsResult < 0) throw new NotValidPolynomialArgumentException($"Initial string contains {calculateBracketsResult} brackets ')' without pair '('");
            if(calculateBracketsResult > 0) throw new NotValidPolynomialArgumentException($"Initial string contains {calculateBracketsResult} brackets '(' without pair ')'");
        }

        private static int CalculateBrackets(string value)
        {
            int n = 0;
            foreach (var c in value)
            {
                if (c == '(')
                {
                    n++;
                }
                else if (c == ')')
                {
                    n--;
                }
            }
            return n;
        }

        private static bool IsContainsOnlyAllowedSymbols(string initialString)
            => !initialString.IsNullOrWhiteSpace() &&
               initialString.All(x => char.IsLetterOrDigit(x) || AllowedSymbols.Contains(x));



        public override string ToString()
        {
            return _initialString;
        }
    }


    /// <summary>
    /// Part of polynomial. 
    /// </summary>
    public class Monomial
    {
        private readonly string _initialMonomialString;

        public Monomial(string initialMonomialString)
        {
            _initialMonomialString = initialMonomialString;
        }

        public override string ToString()
        {
            return _initialMonomialString;
        }
    }
}