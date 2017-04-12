using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.LookupUtility
{
    internal class LookupHelper
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly Dictionary<string, Dictionary<string, string>> _lookupValues = new Dictionary<string, Dictionary<string, string>>();

        internal LookupHelper(ILookupRepository lookupRepository)
        {
            if(lookupRepository == null)
            {
                throw new InvalidOperationException("Lookup repository was not set");
            }

            _lookupRepository = lookupRepository;
        }

        internal string GetValue(string list, string key, bool throwIfNotExists = false)
        {
            if (!_lookupValues.TryGetValue(list, out Dictionary<string, string> dict))
            {
                dict = _lookupRepository.LoadList(list);

                if (dict == null)
                {
                    throw new ArgumentException("The list {0} does not exist.", list);
                }

                _lookupValues.Add(list, dict);
            }

            if (!dict.TryGetValue(key, out string val))
            {
                if (throwIfNotExists)
                {
                    throw new ArgumentException(string.Format("The specified property {0} does not exist in list {1}", key, list));
                }

                return null;
            }

            return val;
        }
    }
}
