using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorWork.Logic.Models
{
    public class playermoves
    {
        private moveList _playerFields;

        public moveList playerFields { get { return _playerFields; } set { _playerFields = playerFields; } }

        
    }

    public enum moveList
    {
        Up,
        Left,
        Down,
        Right
    };
}
