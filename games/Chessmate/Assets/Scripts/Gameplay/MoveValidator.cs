using System.Collections.Generic;
using UnityEngine;
using Chessmate.Board.Rendering;
using Chessmate.Pieces;
using Chessmate.Board.Data.Enums;
using Chessmate.Gameplay;

namespace Chessmate.Gameplay
{
    public class MoveValidator : MonoBehaviour
    {
        private BoardRenderer boardRenderer;

        public static MoveValidator Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            boardRenderer = FindAnyObjectByType<BoardRenderer>();
        }

        public List<BoardTile> GetLegalMoves(PieceView piece)
        {
            List<BoardTile> pseudoMoves =
                MoveGenerator.Instance.GenerateMoves(piece);

            List<BoardTile> legalMoves = new();

            foreach (BoardTile destination in pseudoMoves)
            {
                if (IsMoveLegal(piece, destination))
                    legalMoves.Add(destination);
            }

            return legalMoves;
        }

        private bool IsMoveLegal(
            PieceView piece,
            BoardTile destination)
        {

            if (piece.Type == PieceType.King &&
                Mathf.Abs(destination.Column - piece.CurrentTile.Column) == 2)
            {
                if (!IsCastlingLegal(piece, destination))
                    return false;
            }

            BoardTile source = piece.CurrentTile;
            PieceView captured = destination.OccupyingPiece;

            bool moved = piece.HasMoved;

            // Simulate
            source.ClearPiece();

            if (captured != null)
            {
                captured.CurrentTile.ClearPiece();

                captured.gameObject.SetActive(false);
            }
            destination.SetOccupyingPiece(piece);
            piece.SetTileWithoutEvents(destination);

            if (captured != null)
            {
                Debug.Log("Captured piece still exists: " + captured.name);
            }

            bool check =
                CheckManager.Instance.IsKingInCheck(piece.Color);

            // Undo
            destination.SetOccupyingPiece(captured);

            if (captured != null)
            {
                captured.gameObject.SetActive(true);
                captured.SetTileWithoutEvents(destination);
            }

            piece.SetTileWithoutEvents(source);
            source.SetOccupyingPiece(piece);

            piece.HasMoved = moved;

            return !check;
        }

        private bool IsCastlingLegal(
            PieceView king,
            BoardTile destination)
        {
            // Rule 1:
            // King cannot castle while already in check.
            if (CheckManager.Instance.IsKingInCheck(king.Color))
                return false;

            int row = king.CurrentTile.Row;

            bool kingSide = destination.Column > king.CurrentTile.Column;

            int intermediateColumn = kingSide ? 5 : 3;

            BoardTile intermediateTile =
                boardRenderer.GetTile(row, intermediateColumn);

            // =========================
            // Simulate king on intermediate square
            // =========================

            BoardTile source = king.CurrentTile;

            source.ClearPiece();

            intermediateTile.SetOccupyingPiece(king);
            king.SetTileWithoutEvents(intermediateTile);

            bool intermediateCheck =
                CheckManager.Instance.IsKingInCheck(king.Color);

            // Undo
            intermediateTile.SetOccupyingPiece(null);

            source.SetOccupyingPiece(king);
            king.SetTileWithoutEvents(source);

            if (intermediateCheck)
                return false;

            return true;
        }
    }
}