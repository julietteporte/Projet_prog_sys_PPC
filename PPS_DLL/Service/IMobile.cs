using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public interface IMobile
    {
        Square ActualSquare { get; set; }
        Table ActualTable { get; set; }
        void Move(Square newSquare, Table newTable);
    }
}
