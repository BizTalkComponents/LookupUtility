using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.LookupUtility
{
    public class LookupConfig : IConfiguration
    {
        public bool UseLookupMock()
        {
            return ConfigurationManager.AppSettings["UseLookupMock"] != null;
        }
    }
}
