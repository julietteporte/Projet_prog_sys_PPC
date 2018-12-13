using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public interface IPresenceStrategy
    {
        int GetPresenceTime(int PresenceTime);
    }
}
