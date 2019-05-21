using System;
using System.Linq;
using CanonicalEquation.Extensions;

namespace CanonicalEquation.Parsers
{
    public static class VariableParser
    {
        public static Variable Parse(string variableString)
        {
            int power = 1;

            if(variableString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(variableString));
            var partsOfVariable = variableString.Split(new[] {SymbolsConsts.Power});

            if (partsOfVariable.Length > 2) throw new FormatException("Variable string must contains only one symbol of power '^'");;

            var name = partsOfVariable[0];

            if (name.Length != 1)
            {
                throw new FormatException($"Could not parse name of variable. Name must be a single symbol, but actual: '{name}'");
            }

            if (!char.IsLetter(name.Single()))
            {
                throw new FormatException($"Could not parse name of variable. Name must be a letter symbol, but actual: '{name}'");
            }

            if (partsOfVariable.Length == 2)
            {
                var powStr = partsOfVariable[1];
                if (!int.TryParse(powStr, out power))
                {
                    throw new FormatException($"Could not parse power for variable. Power must be a number, but actual: '{powStr}'");
                }
            }

            return new Variable(name.Single(), power);
        }
    }
}