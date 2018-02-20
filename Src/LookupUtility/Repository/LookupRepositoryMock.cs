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
            return new Dictionary<string, string>() { { "MockKey", "MockValue" } };
        }
    }
}
