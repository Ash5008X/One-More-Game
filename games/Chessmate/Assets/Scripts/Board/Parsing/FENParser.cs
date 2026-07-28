using System;
using Chessmate.Board.Data.Enums;
using Chessmate.Board.Data.Models;

namespace Chessmate.Board.Parsing
{
    /// <summary>
    /// Converts the piece placement section of a FEN string
    /// into a BoardModel.
    /// </summary>
    public class FENParser
    {
        public void Parse(string fen, BoardModel board)
        {
            if (string.IsNullOrWhiteSpace(fen))
                throw new ArgumentException("FEN string cannot be null or empty.");

            string piecePlacement = fen.Split(' ')[0];
            string[] ranks = piecePlacement.Split('/');

            if (ranks.Length != BoardModel.Size)
                throw new ArgumentException("Invalid FEN: Expected 8 ranks.");

            for (int row = 0; row < BoardModel.Size; row++)
            {
                int column = 0;

                foreach (char symbol in ranks[row])
                {
                    if (char.IsDigit(symbol))
                    {
                        column += symbol - '0';
                        continue;
                    }

                    PieceData piece = CreatePiece(symbol);

                    board.GetSquare(row, column).Piece = piece;

                    column++;
                }

                if (column != BoardModel.Size)
                    throw new ArgumentException($"Invalid FEN at rank {row}.");
            }
        }
        private PieceData CreatePiece(char symbol)
        {
            PieceColor color =
                char.IsUpper(symbol)
                ? PieceColor.White
                : PieceColor.Black;

            PieceType type = GetPieceType(char.ToLower(symbol));

            return new PieceData(type, color);
        }
        private PieceType GetPieceType(char symbol)
        {
            return symbol switch
            {
                'p' => PieceType.Pawn,
                'r' => PieceType.Rook,
                'n' => PieceType.Knight,
                'b' => PieceType.Bishop,
                'q' => PieceType.Queen,
                'k' => PieceType.King,
                _ => throw new ArgumentException($"Unknown FEN piece: {symbol}")
            };
        }
    }
}