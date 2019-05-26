using System;
using CanonicalEquation.Entities;

namespace CanonicalEquation
{
    public class Equation
    {
        public Polynomial Left { get; }
        public Polynomial Right { get; }

        public Equation(Polynomial left, Polynomial right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(left)); ;
        }

        public Equation ToCanonicalEquation()
        {
            var canonicalLeft = (Left-Right).Normalize();
            var zeroRight = new Polynomial();
            return new Equation(canonicalLeft, zeroRight);
        }


        public override string ToString()
        {
            return $"{Left}={Right}";
        }
    }
}