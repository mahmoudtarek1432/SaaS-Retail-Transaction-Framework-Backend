using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public enum VersionUpdateTypes
    {
        UpToDate = 1,
        MenuOutDated = 2,
        SettingsOutdated = 3,
        MenuAndSettingsOutdated = 4
    }
}
