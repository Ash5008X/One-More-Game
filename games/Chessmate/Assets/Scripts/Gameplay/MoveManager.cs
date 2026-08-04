using UnityEngine;
using Chessmate.Board.Rendering;
using Chessmate.Pieces;
using Chessmate.Board.Data.Enums;

namespace Chessmate.Gameplay
{
    public class MoveManager : MonoBehaviour
    {
        private BoardRenderer boardRenderer;
        private PieceFactory pieceFactory;
        public static MoveManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            boardRenderer = FindAnyObjectByType<BoardRenderer>();

            pieceFactory = FindAnyObjectByType<PieceFactory>();
        }

        public bool MovePiece(PieceView piece, BoardTile destination)
        {
            if (piece == null || destination == null)
                return false;

            BoardTile fromTile = piece.CurrentTile;

            //============================
            // Capture
            //============================

            if (destination.OccupyingPiece != null)
            {
                CapturePiece(destination.OccupyingPiece);
            }

            //============================
            // Move Piece
            //============================

            piece.transform.position = destination.transform.position;

            piece.MoveTo(destination);

            // ============================
            // Pawn Promotion
            // ============================

            if (piece.Type == PieceType.Pawn)
            {
                bool promote =
                    (piece.Color == PieceColor.White && destination.Row == 0) ||
                    (piece.Color == PieceColor.Black && destination.Row == 7);

                if (promote)
                {
                    PromotePawn(piece);
                }
            }

            // ============================
            // Castling
            // ============================

            if (piece.Type == PieceType.King &&
                Mathf.Abs(destination.Column - fromTile.Column) == 2)
            {
                bool kingSide = destination.Column > fromTile.Column;

                int row = destination.Row;

                BoardTile rookFrom = boardRenderer.GetTile(
                    row,
                    kingSide ? 7 : 0);

                BoardTile rookTo = boardRenderer.GetTile(
                    row,
                    kingSide ? 5 : 3);

                PieceView rook = rookFrom.OccupyingPiece;

                if (rook != null)
                {
                    rook.transform.position = rookTo.transform.position;
                    rook.MoveTo(rookTo);
                }
            }

            //============================
            // Highlights
            //============================

            HighlightManager.Instance.ShowLastMove(
                fromTile,
                destination
            );

            //============================
            // Turn
            //============================

            TurnManager.Instance.EndTurn();
            CheckManager.Instance.UpdateCheckStatus();

            PieceColor currentPlayer =
                TurnManager.Instance.CurrentTurn;

            if (CheckManager.Instance.IsCheckmate(currentPlayer))
            {
                Debug.Log($"CHECKMATE! {currentPlayer} loses.");
            }
            else if (CheckManager.Instance.IsStalemate(currentPlayer))
            {
                Debug.Log("STALEMATE!");
            }

            Debug.Log(
                $"{piece.Color} {piece.Type} moved " +
                $"from ({fromTile.Row}, {fromTile.Column}) " +
                $"to ({destination.Row}, {destination.Column})"
            );

            return true;
        }

        private void CapturePiece(PieceView capturedPiece)
        {
            if (capturedPiece == null)
                return;

            capturedPiece.CurrentTile.ClearPiece();

            Destroy(capturedPiece.gameObject);

            Debug.Log(
                $"Captured {capturedPiece.Color} {capturedPiece.Type}"
            );
        }

        private void PromotePawn(
            PieceView pawn,
            PieceType promotionType = PieceType.Queen)
        {
            BoardTile tile = pawn.CurrentTile;
            PieceColor color = pawn.Color;

            Destroy(pawn.gameObject);

            pieceFactory.CreatePiece(
                promotionType,
                color,
                tile);

            Debug.Log($"{color} Pawn promoted to {promotionType}.");
        }
    }
}