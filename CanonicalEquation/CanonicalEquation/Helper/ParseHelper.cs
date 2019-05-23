using System;
using System.Collections.Generic;
using System.Text;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Extensions;

namespace CanonicalEquation.Helpers
{
    public static class ParseHelper
    {
        /// <summary>
        /// Consists of a collection of expressions in brackets (if there are in the string) and a list of summands (contained between brackets, if any).
        /// If there are no brackets, then the monomial consists of one summand)
        /// </summary>
        /// <param name="monomialString">initial monomial string</param>
        /// <returns>List of monomial item: brackets or summands</returns>
        public static void GetMonomialItemsFromMonomialString(string monomialString, out IList<string> summandList, out IList<string> bracketsList)
        {
            summandList = new List<string>();
            bracketsList = new List<string>();

            var transformedMonomial = monomialString.RemoveWhiteSpaces();
            var monomialItemsStringBuilder = new StringBuilder(transformedMonomial.Length);


            for (int i = 0; i < transformedMonomial.Length; i++)
            {
                var currentSymbol = transformedMonomial[i];

                if (currentSymbol == '(')
                {
                    if (monomialItemsStringBuilder.Length != 0)
                    {
                        var currentSummand = AppentNumberOneForSign(monomialItemsStringBuilder.ToString());
                        summandList.Add(currentSummand);
                        monomialItemsStringBuilder.Clear();
                    }

                    var j = FindClosingBracket(transformedMonomial, i);
                    for (int k = i; k < j + 1; k++)
                    {
                        monomialItemsStringBuilder.Append(transformedMonomial[k]);
                    }
                    bracketsList.Add(monomialItemsStringBuilder.ToString());
                    monomialItemsStringBuilder.Clear();
                    i = j;
                }
                else
                {
                    monomialItemsStringBuilder.Append(currentSymbol);
                }
            }

            if (monomialItemsStringBuilder.Length != 0)
            {
                var currentSummand = AppentNumberOneForSign(monomialItemsStringBuilder.ToString());
                summandList.Add(currentSummand);
            }
        }

        private static string AppentNumberOneForSign(string str)
        {
            if (str.Length == 1 &&
                str[0] == SymbolsConsts.Minus || str[0] == SymbolsConsts.Plus)
            {
                str += "1";
            }

            return str;
        }

        public static IEnumerable<string> GetMonomialsPartsForPolynomialString(string polynomialString)
        {
            var result = new List<string>();
            var monomialItemStringBuilder = new StringBuilder(polynomialString.Length);

            // + or - -> new monomial
            // ' ' ignore
            // '(' - initial search of closing brackets '('
            // '.*^', letters or numbers - part of monomial

            string monomialUtemString;
            for (int i = 0; i < polynomialString.Length; i++)
            {
                var currentSymbol = polynomialString[i];
                if (currentSymbol == '(')
                {
                    var j = FindClosingBracket(polynomialString, i);
                    for (int k = i; k < j + 1; k++)
                    {
                        monomialItemStringBuilder.Append(polynomialString[k]);
                    }

                    i = j;
                }
                
                else if (currentSymbol == SymbolsConsts.Plus || currentSymbol == SymbolsConsts.Minus)
                {
                    if (monomialItemStringBuilder.Length > 0)
                    {
                        monomialUtemString = monomialItemStringBuilder.ToString().RemoveWhiteSpaces();
                        if(!monomialUtemString.IsNullOrWhiteSpace()) result.Add(monomialUtemString);
                        monomialItemStringBuilder.Clear();
                        monomialItemStringBuilder.Append(currentSymbol);
                    }
                }
                else
                {
                    monomialItemStringBuilder.Append(currentSymbol);
                }
            }

            monomialUtemString = monomialItemStringBuilder.ToString().RemoveWhiteSpaces();
            if (!monomialUtemString.IsNullOrWhiteSpace()) result.Add(monomialUtemString);

            return result;
        }

        public static int CalculateBrackets(string value)
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

        public static void ValidateBrackets(string initialValue)
        {
            var calculateBracketsResult = ParseHelper.CalculateBrackets(initialValue);

            if (calculateBracketsResult < 0) throw new NotValidBracketArgumentException($"Initial string contains {calculateBracketsResult} brackets ')' without pair '('");
            if (calculateBracketsResult > 0) throw new NotValidBracketArgumentException($"Initial string contains {calculateBracketsResult} brackets '(' without pair ')'");
        }

        public static string GetContentForBrackets(string bracketsString)
        {
            if (bracketsString.IsNullOrWhiteSpace()) return bracketsString;

            var stringLength = bracketsString.Length;

            if (bracketsString[0] == SymbolsConsts.OpenBracket &&
                bracketsString[stringLength - 1] == SymbolsConsts.CloseBracket)
            {
                return stringLength > 2 ? bracketsString.Substring(1, stringLength - 2) : String.Empty;
            }

            return bracketsString;
        }

        public static int FindClosingBracket(string stringValue, int start)
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

            throw new FormatException($"For bracket '(' missing its counterpart ')'.");
        }
    }
}