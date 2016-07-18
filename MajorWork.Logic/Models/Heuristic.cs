using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorWork.Logic.Models
{
    public class Heuristic 
    {

        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        public int H { get; set; } //Heuristic
        public int G { get; set; } //Cost function from start

    }


}
