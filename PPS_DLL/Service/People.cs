using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public abstract class People
    {
        public abstract void Wait();
        public abstract int Id { get; }
    }
}
