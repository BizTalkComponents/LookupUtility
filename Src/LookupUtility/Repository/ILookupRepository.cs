using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Repository
{
    public interface ILookupRepository
    {
        Dictionary<string, string> LoadList(string list);
    }
}
