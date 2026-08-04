using System.Collections.Generic;
using UnityEngine;
using Chessmate.Pieces;
using Chessmate.Board.Rendering;

namespace Chessmate.Gameplay
{
    public class SelectionManager : MonoBehaviour
    {
        public static SelectionManager Instance { get; private set; }

        public PieceView SelectedPiece { get; private set; }

        private readonly List<BoardTile> legalMoves = new();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SelectPiece(PieceView piece)
        {
            if (piece == null)
                return;

            if (!TurnManager.Instance.IsPlayersTurn(piece.Color))
                return;

            if (SelectedPiece == piece)
                return;

            DeselectCurrent();

            SelectedPiece = piece;
            SelectedPiece.Select();

            HighlightManager.Instance.ShowSelectedTile(
                SelectedPiece.CurrentTile);

            legalMoves.Clear();
            legalMoves.AddRange(
                MoveValidator.Instance.GetLegalMoves(piece));

                Debug.Log("LEGAL MOVES");

                foreach (BoardTile move in legalMoves)
                {
                    Debug.Log($"{move.Row},{move.Column}");
                }

            HighlightManager.Instance.ShowMoves(legalMoves);

            Debug.Log(
                $"Selected {piece.Color} {piece.Type} at ({piece.CurrentTile.Row}, {piece.CurrentTile.Column})");
        }

        public void TryMove(BoardTile destination)
        {
            if (SelectedPiece == null)
                return;

            if (!legalMoves.Contains(destination))
                return;

            PieceView piece = SelectedPiece;

            DeselectCurrent();

            MoveManager.Instance.MovePiece(
                piece,
                destination);
        }

        public void DeselectCurrent()
        {
            if (SelectedPiece == null)
                return;

            SelectedPiece.Deselect();

            HighlightManager.Instance.ClearMoveHighlights();
            HighlightManager.Instance.ClearSelectedTile();

            legalMoves.Clear();

            Debug.Log(
                $"Deselected {SelectedPiece.Color} {SelectedPiece.Type}");

            SelectedPiece = null;
        }

        public bool HasSelection()
        {
            return SelectedPiece != null;
        }

        public bool IsLegalMove(BoardTile tile)
        {
            return legalMoves.Contains(tile);
        }
    }
}