using System;
using System.Collections.Generic;
using System.Configuration;

namespace Shared.Utilities.LookupUtility
{
    public class LookupUtilityService
    {

        private ILookupRepository _lookupRepository;
        private LookupHelper _lookupHelper;
        private IConfiguration _configuration;

        public LookupUtilityService()
        {

        }

        public IConfiguration Config
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new LookupConfig();
                }

                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }

        public LookupUtilityService(ILookupRepository lookupRepository)
        {
            if (lookupRepository == null)
            {
                throw new InvalidOperationException("Lookup repository was not correctly set");
            }

            _lookupRepository = lookupRepository;
        }

        public string GetValue(string list, string key, bool throwIfNotExists = false)
        {
            if (_lookupRepository == null)
            {
                _lookupRepository = ResolveRepository();
            }
            if (_lookupHelper == null)
            {
                _lookupHelper = new LookupHelper(_lookupRepository);
            }

            return _lookupHelper.GetValue(list, key, throwIfNotExists);
        }

        private ILookupRepository ResolveRepository()
        {
            if (_configuration.UseLookupMock())
            {
                return new LookupRepositoryMock();
            }
            else
            {
                return new SharepointLookupRepository();
            }
        }
    }
}
