namespace Chessmate.Board.Data.Models
{
    public class BoardModel
    {
        public const int Size = 8;

        public Square[,] Squares { get; }

        public BoardModel()
        {
            Squares = new Square[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Squares[row, col] = new Square(row, col);
                }
            }
        }

        public Square GetSquare(int row, int col)
        {
            return Squares[row, col];
        }
    }
}