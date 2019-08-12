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
        private bool initialized;
        public SqlApplicationService()
        {
            
        }
        public string SetConnection(string connection=default(string))
        {
            if (string.IsNullOrEmpty(connection)) connection = "SqlLookupRepository";
            svc = new LookupUtilityService(new SqlLookupRepository(connection));
            initialized = true;
            return "success";
        }

        public string GetValue(string list, string key, bool throwIfNotExists = false, bool allowDefaults = false)
        {
            if (!initialized) SetConnection();
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
            if (!initialized) SetConnection();
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

        public System.Xml.XmlElement GetListAsXml(string list, bool throwIfNotExists = false)
        {
            if (!initialized) SetConnection();
            return svc.GetListAsXml(list);
        }
    }
}
