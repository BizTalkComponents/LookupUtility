﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Application
{
    public interface IApplicationService
    {
        string GetValue(string list, string key, bool throwIfNotExists = false, bool allowDefaults = false);
    }
}
