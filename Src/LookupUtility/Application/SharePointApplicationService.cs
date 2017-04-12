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
            bool isBizTalk = Process.GetCurrentProcess()
                                .ProcessName.ToLower().Contains("btsntsvc.exe");
            //if(isBizTalk)
            //{ 
            svc = new LookupUtilityService(new SharepointLookupRepository());
            //}
            //else
            //{
            //svc = new LookupUtilityService(new LookupRepositoryMock());
            //}
        }
        public string GetValue(string list, string key, bool throwIfNotExists = false)
        {
            return svc.GetValue(list, key, throwIfNotExists);
        }
    }
}
