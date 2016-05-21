using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorWork.Logic.Models
{
    class astar : stack
    {
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
    }
}
