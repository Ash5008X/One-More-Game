using Chessmate.Board.Data.Enums;

namespace Chessmate.Board.Data.Models
{
    public class PieceData
    {
        public PieceType Type { get; }

        public PieceColor Color { get; }

        public PieceData(PieceType type, PieceColor color)
        {
            Type = type;
            Color = color;
        }

        public bool IsEmpty =>
            Type == PieceType.None;
    }
}