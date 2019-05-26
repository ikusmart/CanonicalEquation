using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CanonicalEquation.Parsers
{
    public class ParseResult
    {
        private readonly IList<string> _errorMessages = new List<string>();

        public void AddError(string errorMessage)
        {
            _errorMessages.Add(errorMessage);
        }

        public bool IsSucceed => !_errorMessages.Any();

        public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errorMessages);
    }
}