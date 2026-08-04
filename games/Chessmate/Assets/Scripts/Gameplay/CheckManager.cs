using System.Collections.Generic;
using UnityEngine;
using Chessmate.Board.Data.Enums;
using Chessmate.Board.Rendering;
using Chessmate.Pieces;

namespace Chessmate.Gameplay
{
    public class CheckManager : MonoBehaviour
    {
        public static CheckManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void UpdateCheckStatus()
        {
            HighlightManager.Instance.ClearCheck();

            CheckKing(PieceColor.White);
            CheckKing(PieceColor.Black);
        }

        public bool IsKingInCheck(PieceColor kingColor)
        {
            PieceView king = FindKing(kingColor);

            if (king == null)
                return false;

            PieceColor attackerColor =
                kingColor == PieceColor.White
                ? PieceColor.Black
                : PieceColor.White;

            return IsTileUnderAttack(
                king.CurrentTile,
                attackerColor);
        }

        private void CheckKing(PieceColor kingColor)
        {
            PieceView king = FindKing(kingColor);

            if (king == null)
                return;

            PieceColor attackerColor =
                kingColor == PieceColor.White
                ? PieceColor.Black
                : PieceColor.White;

            if (IsTileUnderAttack(
                king.CurrentTile,
                attackerColor))
            {
                HighlightManager.Instance.ShowCheck(
                    king.CurrentTile);

                Debug.Log($"{kingColor} King is in Check.");
            }
        }

        private bool IsTileUnderAttack(
            BoardTile tile,
            PieceColor attackerColor)
        {
            PieceView[] pieces =
                FindObjectsByType<PieceView>(FindObjectsInactive.Exclude);

            foreach (PieceView piece in pieces)
            {
                if (piece.Color != attackerColor)
                    continue;

                Debug.Log(
                    $"Checking {piece.Color} {piece.Type} " +
                    $"CurrentTile={(piece.CurrentTile == null ? "NULL" : $"{piece.CurrentTile.Row},{piece.CurrentTile.Column}")}");
                
                List<BoardTile> moves =
                    MoveGenerator.Instance.GenerateMoves(piece);

                foreach (BoardTile move in moves)
                {
                    Debug.Log($"    -> ({move.Row},{move.Column})");
                }

                if (moves.Contains(tile))
                {
                    Debug.Log(
                        $"ATTACK FOUND by {piece.Color} {piece.Type}");

                    return true;
                }
            }

            return false;
        }

        private PieceView FindKing(PieceColor color)
        {
            PieceView[] pieces =
                FindObjectsByType<PieceView>(FindObjectsInactive.Exclude);

            foreach (PieceView piece in pieces)
            {
                if (piece.Color == color &&
                    piece.Type == PieceType.King)
                {
                    return piece;
                }
            }

            return null;
        }

        public bool IsCheckmate(PieceColor color)
        {
            if (!IsKingInCheck(color))
                return false;

            return !HasAnyLegalMove(color);
        }

        public bool IsStalemate(PieceColor color)
        {
            if (IsKingInCheck(color))
                return false;

            return !HasAnyLegalMove(color);
        }

        private bool HasAnyLegalMove(PieceColor color)
        {
            PieceView[] pieces =
                FindObjectsByType<PieceView>(FindObjectsInactive.Exclude);

            foreach (PieceView piece in pieces)
            {
                if (piece.Color != color)
                    continue;

                if (MoveValidator.Instance
                    .GetLegalMoves(piece).Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}