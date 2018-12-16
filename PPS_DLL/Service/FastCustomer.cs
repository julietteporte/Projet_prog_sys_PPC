using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class FastStrategy : IPresenceStrategy
    {
        public int GetPresenceTime(int presenceTime)
        {
            return presenceTime / 2;
        }
    }
}
