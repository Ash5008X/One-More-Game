using System.Collections.Generic;
using UnityEngine;
using Chessmate.Board.Rendering;
using Chessmate.Board.Data.Enums;
using Chessmate.Pieces;

namespace Chessmate.Gameplay
{
    public class MoveGenerator : MonoBehaviour
    {
        public static MoveGenerator Instance { get; private set; }

        private BoardRenderer boardRenderer;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            boardRenderer = FindAnyObjectByType<BoardRenderer>();
        }

        public List<BoardTile> GenerateMoves(PieceView piece)
        {
            List<BoardTile> moves = new();

            switch (piece.Type)
            {
                case PieceType.Pawn:
                    GeneratePawnMoves(piece, moves);
                    break;

                case PieceType.Knight:
                    GenerateKnightMoves(piece, moves);
                    break;

                case PieceType.Bishop:
                    GenerateSlidingMoves(piece, moves,
                        new Vector2Int(1,1),
                        new Vector2Int(1,-1),
                        new Vector2Int(-1,1),
                        new Vector2Int(-1,-1));
                    break;

                case PieceType.Rook:
                    GenerateSlidingMoves(piece, moves,
                        new Vector2Int(1,0),
                        new Vector2Int(-1,0),
                        new Vector2Int(0,1),
                        new Vector2Int(0,-1));
                    break;

                case PieceType.Queen:
                    GenerateSlidingMoves(piece, moves,
                        new Vector2Int(1,0),
                        new Vector2Int(-1,0),
                        new Vector2Int(0,1),
                        new Vector2Int(0,-1),
                        new Vector2Int(1,1),
                        new Vector2Int(1,-1),
                        new Vector2Int(-1,1),
                        new Vector2Int(-1,-1));
                    break;

                case PieceType.King:
                    GenerateKingMoves(piece, moves);
                    break;
            }

            return moves;
        }

        #region Pawn

        private void GeneratePawnMoves(PieceView piece, List<BoardTile> moves)
        {
            int direction = piece.Color == PieceColor.White ? -1 : 1;

            int row = piece.CurrentTile.Row;
            int column = piece.CurrentTile.Column;

            BoardTile forward = GetTile(row + direction, column);

            if (forward != null && forward.OccupyingPiece == null)
            {
                moves.Add(forward);

                if (!piece.HasMoved)
                {
                    BoardTile doubleForward = GetTile(row + direction * 2, column);

                    if (doubleForward != null &&
                        doubleForward.OccupyingPiece == null)
                    {
                        moves.Add(doubleForward);
                    }
                }
            }

            TryAddCapture(piece, row + direction, column - 1, moves);
            TryAddCapture(piece, row + direction, column + 1, moves);
        }

        #endregion

        #region Knight

        private void GenerateKnightMoves(PieceView piece, List<BoardTile> moves)
        {
            int row = piece.CurrentTile.Row;
            int column = piece.CurrentTile.Column;

            Vector2Int[] offsets =
            {
                new(2,1),
                new(2,-1),
                new(-2,1),
                new(-2,-1),
                new(1,2),
                new(1,-2),
                new(-1,2),
                new(-1,-2)
            };

            foreach (var offset in offsets)
            {
                TryAddMove(piece,
                    row + offset.x,
                    column + offset.y,
                    moves);
            }
        }

        #endregion

        #region King

        private void GenerateKingMoves(PieceView piece, List<BoardTile> moves)
        {
            int row = piece.CurrentTile.Row;
            int column = piece.CurrentTile.Column;

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0)
                        continue;

                    TryAddMove(piece,
                        row + r,
                        column + c,
                        moves);
                }
            }

            GenerateCastlingMoves(piece, moves);
        }

        private void GenerateCastlingMoves(
            PieceView king,
            List<BoardTile> moves)
        {
            // King must not have moved
            if (king.HasMoved)
                return;

            int row = king.Color == PieceColor.White ? 7 : 0;

            // ============================
            // Kingside Castling
            // ============================

            BoardTile kingsideRookTile = GetTile(row, 7);

            if (kingsideRookTile != null &&
                kingsideRookTile.OccupyingPiece != null)
            {
                PieceView rook = kingsideRookTile.OccupyingPiece;

                if (rook.Type == PieceType.Rook &&
                    rook.Color == king.Color &&
                    !rook.HasMoved)
                {
                    BoardTile fTile = GetTile(row, 5);
                    BoardTile gTile = GetTile(row, 6);

                    if (fTile.OccupyingPiece == null &&
                        gTile.OccupyingPiece == null)
                    {
                        moves.Add(gTile);
                    }
                }
            }

            // ============================
            // Queenside Castling
            // ============================

            BoardTile queensideRookTile = GetTile(row, 0);

            if (queensideRookTile != null &&
                queensideRookTile.OccupyingPiece != null)
            {
                PieceView rook = queensideRookTile.OccupyingPiece;

                if (rook.Type == PieceType.Rook &&
                    rook.Color == king.Color &&
                    !rook.HasMoved)
                {
                    BoardTile dTile = GetTile(row, 3);
                    BoardTile cTile = GetTile(row, 2);
                    BoardTile bTile = GetTile(row, 1);

                    if (dTile.OccupyingPiece == null &&
                        cTile.OccupyingPiece == null &&
                        bTile.OccupyingPiece == null)
                    {
                        moves.Add(cTile);
                    }
                }
            }
        }

        #endregion

        #region Sliding Pieces

        private void GenerateSlidingMoves(
            PieceView piece,
            List<BoardTile> moves,
            params Vector2Int[] directions)
        {
            foreach (Vector2Int direction in directions)
            {
                int row = piece.CurrentTile.Row + direction.x;
                int column = piece.CurrentTile.Column + direction.y;

                while (true)
                {
                    BoardTile tile = GetTile(row, column);

                    if (tile == null)
                        break;

                    if (tile.OccupyingPiece == null)
                    {
                        moves.Add(tile);
                    }
                    else
                    {
                        if (tile.OccupyingPiece.Color != piece.Color)
                        {
                            Debug.Log(
                                $"{piece.Type} can capture {tile.OccupyingPiece.Type} at ({tile.Row},{tile.Column})");

                            moves.Add(tile);
                        }

                        break;
                    }

                    row += direction.x;
                    column += direction.y;
                }
            }
        }

        #endregion

        #region Helpers

        private BoardTile GetTile(int row, int column)
        {
            return boardRenderer.GetTile(row, column);
        }

        private void TryAddMove(
            PieceView piece,
            int row,
            int column,
            List<BoardTile> moves)
        {
            BoardTile tile = GetTile(row, column);

            if (tile == null)
                return;

            if (tile.OccupyingPiece == null)
            {
                moves.Add(tile);
                return;
            }

            if (tile.OccupyingPiece.Color != piece.Color)
                moves.Add(tile);
        }

        private void TryAddCapture(
            PieceView piece,
            int row,
            int column,
            List<BoardTile> moves)
        {
            BoardTile tile = GetTile(row, column);

            if (tile == null)
                return;

            if (tile.OccupyingPiece == null)
                return;

            if (tile.OccupyingPiece.Color != piece.Color)
                moves.Add(tile);
        }

        #endregion
    }
}