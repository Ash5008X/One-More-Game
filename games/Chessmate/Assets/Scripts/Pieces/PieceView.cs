using UnityEngine;
using Chessmate.Board.Data.Enums;
using Chessmate.Board.Rendering;

namespace Chessmate.Pieces
{
    public class PieceView : MonoBehaviour
    {
        public PieceType Type { get; private set; }

        public PieceColor Color { get; private set; }

        public BoardTile CurrentTile { get; private set; }

        public bool HasMoved { get; set; }

        public bool IsSelected { get; private set; }

        private PieceRenderer pieceRenderer;

        private void Awake()
        {
            pieceRenderer = GetComponent<PieceRenderer>();
        }

        public void Initialize(PieceType type, PieceColor color, BoardTile tile)
        {
            Type = type;
            Color = color;
            CurrentTile = tile;
            tile.SetOccupyingPiece(this);

            HasMoved = false;
            IsSelected = false;

            pieceRenderer.SetColor(color);
        }

        public void SetTile(BoardTile tile)
        {
            CurrentTile = tile;
        }

        public void MoveTo(BoardTile tile)
        {
            if (CurrentTile != null)
                CurrentTile.ClearPiece();

            CurrentTile = tile;

            if (CurrentTile != null)
                CurrentTile.SetOccupyingPiece(this);

            HasMoved = true;
        }

        public void Select()
        {
            IsSelected = true;
        }

        public void Deselect()
        {
            IsSelected = false;
        }

        public void SetTileWithoutEvents(BoardTile tile)
        {
            CurrentTile = tile;
        }
    }
}