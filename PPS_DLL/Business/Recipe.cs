using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Business
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PrepTime { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
    }
}
