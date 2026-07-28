using UnityEngine;
using Chessmate.Core;
using Chessmate.Board.Data.Models;
using Chessmate.Board.Parsing;

namespace Chessmate.Managers
{
    public class BoardManager : IGameService
    {
        public BoardModel Board { get; private set; }
        private readonly FENParser fenParser = new();

        public void Initialize()
        {
            Debug.Log("Initializing Board Manager...");

            Board = new BoardModel();

            Debug.Log($"Board Created ({BoardModel.Size} x {BoardModel.Size})");
        }

        public Square GetSquare(int row, int col)
        {
            return Board.GetSquare(row, col);
        }

        public void LoadPosition(string fen)
        {
            fenParser.Parse(fen, Board);

            Debug.Log("Board position loaded.");
        }
    }
}