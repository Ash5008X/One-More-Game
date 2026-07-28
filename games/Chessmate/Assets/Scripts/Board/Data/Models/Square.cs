namespace Chessmate.Board.Data.Models
{
    public class Square
    {
        public int Row { get; }

        public int Column { get; }

        public PieceData Piece { get; set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;

            Piece = new PieceData(
                Enums.PieceType.None,
                Enums.PieceColor.None
            );
        }
    }
}