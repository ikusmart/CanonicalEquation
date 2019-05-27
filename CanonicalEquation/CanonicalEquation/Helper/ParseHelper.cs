using System;
using System.Collections.Generic;
using System.Text;
using CanonicalEquation.Lib.Extensions;
using CanonicalEquation.Lib.Exceptions;

namespace CanonicalEquation.Lib.Helpers
{
    public static class ParseHelper
    {
        public static IEnumerable<string> GetMonomialssForPolynomial(string polynomialString)
        {
            var result = new List<string>();
            var summandStringBuilder = new StringBuilder(polynomialString.Length);
            polynomialString = polynomialString.RemoveWhiteSpaces();
            // + or - -> new monomial
            // ' ' ignore
            // '(' - initial search of closing brackets '('
            // '.*^', letters or numbers - part of monomial

            string summandItemString;
            for (int i = 0; i < polynomialString.Length; i++)
            {
                var currentSymbol = polynomialString[i];
                if (currentSymbol == '(')
                {
                    var j = FindClosingBracket(polynomialString, i);
                    if(j == -1) throw new FormatException($"For bracket '(' missing its counterpart ')'.");

                    for (int k = i; k < j + 1; k++)
                    {
                        summandStringBuilder.Append(polynomialString[k]);
                    }
                    i = j;
                }

                else if (currentSymbol == Symbols.Plus || currentSymbol == Symbols.Minus )
                {
                    if (summandStringBuilder.Length > 0)
                    {
                        summandItemString = summandStringBuilder.ToString();
                        if (!summandItemString.IsNullOrWhiteSpace())
                        {
                            if (!summandItemString.Equals(Symbols.Minus.ToString()) && !summandItemString.Equals(Symbols.Plus.ToString()))
                            {
                                result.Add(summandItemString); 
                            }
                        }
                        summandStringBuilder.Clear();
                    }
                    summandStringBuilder.Append(currentSymbol);
                }
                else
                {
                    summandStringBuilder.Append(currentSymbol);
                }
            }

            if (summandStringBuilder.Length > 0)
            {
                summandItemString = summandStringBuilder.ToString();
                if (!summandItemString.IsNullOrWhiteSpace())
                {
                    if (!summandItemString.Equals(Symbols.Minus.ToString()) && !summandItemString.Equals(Symbols.Plus.ToString()))
                    {
                        result.Add(summandItemString);
                    }
                }
                summandStringBuilder.Clear();
            }

            return result;
        }

        /// <summary>
        /// Consists of a collects of expressions in brackets (if there are in the string) and a list of summands (contained between brackets, if any).
        /// If there are no brackets, then the monomial consists of one summand)
        /// </summary>
        /// <param name="monomialString">initial monomial string</param>
        /// <returns>List of monomial item: brackets or summands</returns>
        public static (IList<string> summandItems, IList<string> bracketsParts) GetSummandsForMonomial(
            string monomialString)
        {
            var summandList = new List<string>();
            var bracketsList = new List<string>();

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
                    if (j != (i+1)  )
                    {
                        for (int k = i + 1; k < j; k++)
                        {
                            monomialItemsStringBuilder.Append(transformedMonomial[k]);
                        }
                        bracketsList.Add(monomialItemsStringBuilder.ToString());
                    }
                    
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

            return (summandList, bracketsList);
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

            return -1;
        }

        private static string AppentNumberOneForSign(string str)
        {
            if (str.Length == 1 &&
                (str[0] == Symbols.Minus || str[0] == Symbols.Plus))
            {
                str += "1";
            }

            return str;
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

        public static bool IsBrackets(string bracketsStr)
            => !bracketsStr.IsNullOrWhiteSpace()
               && bracketsStr.Length > 1
               && bracketsStr[0] == Symbols.OpenBracket
               && bracketsStr[bracketsStr.Length - 1] == Symbols.CloseBracket;

        public static string GetContentForBrackets(string bracketsString)
        {
            if (!IsBrackets(bracketsString)) return bracketsString;

            var stringLength = bracketsString.Length;
            return stringLength > 2 ? bracketsString.Substring(1, stringLength - 2) : String.Empty;
        }

    }
}