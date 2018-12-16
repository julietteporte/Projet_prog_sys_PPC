using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Business
{
    public class Actions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public People People { get; set; }
        public int Time { get; set; }
    }
}
