using BizTalkComponents.Utilities.LookupUtility.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Application
{
    public class SharePointApplicationService : IApplicationService
    {
        private LookupUtilityService svc;

        public SharePointApplicationService()
        {
            //Todo: Check if code is run from Visual Studio or BizTalk.
            //bool isBizTalk = Process.GetCurrentProcess()
            //                    .ProcessName.ToLower().Contains("btsntsvc.exe");
            //if(isBizTalk)
            //{ 
            svc = new LookupUtilityService(new SharepointLookupRepository());
            //}
            //else
            //{
            //svc = new LookupUtilityService(new LookupRepositoryMock());
            //}
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
