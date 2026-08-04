using UnityEngine;
using Chessmate.Board.Rendering;
using Chessmate.Board.Data.Models;

namespace Chessmate.Gameplay
{
    public class BoardMapper : MonoBehaviour
    {
        public static BoardMapper Instance { get; private set; }

        private BoardTile[,] tiles = new BoardTile[8, 8];

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void RegisterTile(BoardTile tile)
        {
            tiles[tile.Row, tile.Column] = tile;
            Debug.Log($"Registered Tile {tile.Row},{tile.Column}");
        }

        public BoardTile GetTile(int row, int column)
        {
            if (row < 0 || row >= 8 || column < 0 || column >= 8)
                return null;

            return tiles[row, column];
        }

        public BoardTile GetTile(Square square)
        {
            return GetTile(square.Row, square.Column);
        }
    }
}