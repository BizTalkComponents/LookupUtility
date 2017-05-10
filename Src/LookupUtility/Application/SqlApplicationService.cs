using BizTalkComponents.Utilities.LookupUtility.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Application
{
    public class SqlApplicationService : IApplicationService
    {
        private LookupUtilityService svc;

        public SqlApplicationService()
        {
            svc = new LookupUtilityService(new SqlLookupRepository());
        }

        public string GetValue(string list, string key, bool throwIfNotExists = false, bool allowDefaults = false)
        {
            string value;

            try
            {
                value = svc.GetValue(list, key, throwIfNotExists, allowDefaults);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("An exception was thrown in LookupUtility {0}", ex.ToString()));
                throw ex;
            }

            return value;
        }

        public string GetValue(string list, string key, string defaultValue)
        {
            string value;

            try
            {
                value = svc.GetValue(list, key, defaultValue);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("An exception was thrown in LookupUtility {0}", ex.ToString()));
                throw ex;
            }

            return value;
        }
    }
}
