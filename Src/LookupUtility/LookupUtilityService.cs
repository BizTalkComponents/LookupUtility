using BizTalkComponents.Utilities.LookupUtility.Repository;
using System;
using System.Collections.Generic;

namespace BizTalkComponents.Utilities.LookupUtility
{
    public class LookupUtilityService
    {
        private ILookupRepository _lookupRepository;
        private readonly Dictionary<string, Dictionary<string, string>> _lookupValues = new Dictionary<string, Dictionary<string, string>>();
        private const string DEFAULT_KEY = "default";

        public LookupUtilityService(ILookupRepository lookupRepository)
        {
            if(lookupRepository == null)
            {
                throw new InvalidOperationException("LookupRepository is not set.");
            }

            _lookupRepository = lookupRepository;
        }

        public string GetValue(string list, string key, bool throwIfNotExists = false, bool allowDefaults = false)
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

                if (allowDefaults && dict.TryGetValue(DEFAULT_KEY, out string defaultValue))
                {
                    return defaultValue;
                }

                return null;
            }

            return val;
        }
    }
}
