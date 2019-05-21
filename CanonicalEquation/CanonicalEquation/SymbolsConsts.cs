namespace CanonicalEquation
{
    public class SymbolsConsts
    {
        public const char Equality = '=';
        public const char Minus = '-';
        public const char Plus = '+';
        public const char Power = '^';
        public const char OpenBracket = '(';
        public const char CloseBracket = ')';
        public const char Dot = '.';
        public const char Multiplication = '*';

        public const char Space = ' ';

        public char[] AllowedSymbolsForEquation = new[]
            {Equality, Minus, Plus, Power, OpenBracket, CloseBracket, Dot, Multiplication};
    }
}