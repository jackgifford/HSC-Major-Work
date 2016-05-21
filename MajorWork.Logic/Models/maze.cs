using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajorWork.Logic.Models;
using MajorWork.Logic.Helpers;

namespace MajorWork.Logic.Models
{
    public class maze : ObservableObject
    {
        private int _width;
        private int _length;
        private List<stack> _mazeStack;
        private List<mazepoints> _mazePoints;


        //TODO: Add validation to width, and length to ensure they are within range
        public int width { get { return _width; } set { _width = width; } }
        public int length { get { return _length; } set { _length = length; } } 
        public List<stack> mazeStack { get { return _mazeStack; } set { _mazeStack = mazeStack; } }
        public List<mazepoints> mazeGrid { get; set; }
    }
}
