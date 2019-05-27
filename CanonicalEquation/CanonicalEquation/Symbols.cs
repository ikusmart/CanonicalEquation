namespace CanonicalEquation.Lib
{
    public class Symbols
    {
        public const char Equality = '=';
        public const char Minus = '-';
        public const char Plus = '+';
        public const char Power = '^';
        public const char OpenBracket = '(';
        public const char CloseBracket = ')';
        public const char Dot = '.';

        public const char Space = ' ';

        public static char[] AllowedSymbolsForEquation = new[] { Equality, Minus, Plus, Power, OpenBracket, CloseBracket, Dot, Space };
    }
}