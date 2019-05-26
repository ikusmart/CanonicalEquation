using CanonicalEquation.Exceptions;

namespace CanonicalEquation.Entities
{
    /// <summary>
    /// Variable in equation
    /// Example:
    ///     x^3
    ///     y
    ///     z^5
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Name of variable
        ///     For 'x^3' name is 'x'
        /// </summary>
        public char Name { get; }

        /// <summary>
        /// Power of variable
        ///     default: 1 (for example 'x')
        ///     For 'x^3' power is '3'
        /// </summary>
        public int Power { get; }

        public Variable(char name, int power = 1)
        {
            if (!char.IsLetter(name)) throw new NotValidVariableArgumentException("Name of variable must be a letter", nameof(name));

            Name = name;
            Power = power;
        }

        public override string ToString()
        {
            return Power == 1 ? Name.ToString() : $"{Name}^{Power}";
        }

    }
}