using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.LookupUtility
{
    public class LookupRepositoryMock : ILookupRepository
    {
        public Dictionary<string, string> LoadList(string list)
        {
            return new Dictionary<string, string>() { { "MockKey", "MockValue" } };
        }
    }
}
