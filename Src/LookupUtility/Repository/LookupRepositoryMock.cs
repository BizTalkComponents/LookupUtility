using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Repository
{
    public class LookupRepositoryMock : ILookupRepository
    {
        public Dictionary<string, string> LoadList(string list, TimeSpan maxAge = default(TimeSpan))
        {
            if (maxAge != default(TimeSpan) && maxAge > new TimeSpan(1, 0, 0)) 
                throw new ArgumentException("List too old");
            return new Dictionary<string, string>() { { "MockKey", "MockValue" } };
        }
    }
}
