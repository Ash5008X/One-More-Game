using UnityEngine;
using Chessmate.Board.Data.Models;
using Chessmate.Pieces;

namespace Chessmate.Board.Rendering
{
    public class BoardRenderer : MonoBehaviour
    {
        private const int BoardSize = 8;
        private const float TileSize = 1f;

        [Header("Board Assets")]
        [SerializeField] private GameObject boardTilePrefab;
        [SerializeField] private Material whiteTileMaterial;
        [SerializeField] private Material blackTileMaterial;

        private BoardTile[,] tiles;

        private Transform tilesParent;
        private Transform piecesParent;

        private PieceFactory pieceFactory;

        public Transform PiecesParent => piecesParent;

        private void Awake()
        {
            tiles = new BoardTile[BoardSize, BoardSize];

            tilesParent = new GameObject("Tiles").transform;
            tilesParent.SetParent(transform, false);

            piecesParent = new GameObject("Pieces").transform;
            piecesParent.SetParent(transform, false);

            pieceFactory = FindAnyObjectByType<PieceFactory>();
        }

        public void GenerateBoard()
        {
            float boardOffset = (BoardSize - 1) * TileSize * 0.5f;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    CreateTile(row, column, boardOffset);
                }
            }

            Debug.Log("Board rendered successfully.");
        }

        private void CreateTile(int row, int column, float boardOffset)
        {
            Vector3 position = new Vector3(
                column * TileSize - boardOffset,
                0f,
                row * TileSize - boardOffset
            );

            GameObject tileObject = Instantiate(
                boardTilePrefab,
                position,
                Quaternion.identity,
                tilesParent
            );

            BoardTile tile = tileObject.GetComponent<BoardTile>();

            Material material =
                (row + column) % 2 == 0
                ? whiteTileMaterial
                : blackTileMaterial;

            tile.Initialize(
                row,
                column,
                material
            );

            tiles[row, column] = tile;
        }

        public BoardTile GetTile(int row, int column)
        {
            if (row < 0 || row >= BoardSize)
                return null;

            if (column < 0 || column >= BoardSize)
                return null;

            return tiles[row, column];
        }

        public void RenderPosition(BoardModel board)
        {
            if (pieceFactory == null)
            {
                Debug.LogError("PieceFactory not found!");
                return;
            }

            for (int row = 0; row < BoardModel.Size; row++)
            {
                for (int column = 0; column < BoardModel.Size; column++)
                {
                    Square square = board.GetSquare(row, column);

                    if (square.Piece.IsEmpty)
                        continue;

                    pieceFactory.CreatePiece(
                        square.Piece.Type,
                        square.Piece.Color,
                        GetTile(row, column)
                    );
                }
            }
        }
    }
}