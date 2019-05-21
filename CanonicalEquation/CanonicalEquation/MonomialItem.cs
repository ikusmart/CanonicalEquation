namespace CanonicalEquation
{
    /// <summary>
    /// One of the items of the monomial. The items in the monomial are multiplied together
    /// </summary>
    public abstract class MonomialItem
    {
        public float Multiplier { get; set; }

        protected MonomialItem(float multiplier)
        {
            Multiplier = multiplier;
        }

        
    }
}