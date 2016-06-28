

namespace MajorWork.Logic.Models
{
    public class PlayerMoves
    {
        private MoveList _playerFields;

        public MoveList PlayerFields { get { return _playerFields; } set { _playerFields = PlayerFields; } }

        
    }

    public enum MoveList
    {
        Up,
        Left,
        Down,
        Right
    };
}
