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
        private bool initialized;

        public SharePointApplicationService()
        {
            Trace.WriteLine("Initializing sharepoint service");
            //Todo: Check if code is run from Visual Studio or BizTalk.
            //bool isBizTalk = Process.GetCurrentProcess()
            //                    .ProcessName.ToLower().Contains("btsntsvc.exe");
            //if(isBizTalk)
            //{             
            //}
            //else
            //{
            //svc = new LookupUtilityService(new LookupRepositoryMock());
            //}
        }
               
        public string SetConnection(string connection=default(string))
        {
            Trace.WriteLine("SetConnection");
            if (string.IsNullOrEmpty(connection)) connection = "SharePointSite";
            svc = new LookupUtilityService(new SharepointLookupRepository(connection));
            initialized = true;
            return "success";
        }

        public string GetValue(string list, string key, bool throwIfNotExists = false, bool allowDefaults = false)
        {
            Trace.WriteLine($"Getting value for list {list} and key {key}");
            string value;
            if (!initialized) SetConnection();
            try
            {
                value = svc.GetValue(list, key, throwIfNotExists, allowDefaults);
                Trace.WriteLine($"Value returned {value}");
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
            Trace.WriteLine("Getting value other overload");
            if (!initialized) SetConnection();
            string value;

            try
            {
                value = svc.GetValue(list, key, defaultValue);
                Trace.WriteLine($"Got value {value}");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("An exception was thrown in LookupUtility {0}", ex.ToString()));
                throw ex;
            }

            return value;
        }

        public System.Xml.XmlElement GetListAsXml(string list, bool throwIfNotExists = false)
        {
            if (!initialized) SetConnection();
            return svc.GetListAsXml(list);
        }

    }
}
