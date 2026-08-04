using UnityEngine;
using Chessmate.Board.Data.Enums;
using Chessmate.Board.Rendering;

namespace Chessmate.Pieces
{
    public class PieceFactory : MonoBehaviour
    {
        [Header("Piece Prefabs")]

        [SerializeField] private PieceView pawn;
        [SerializeField] private PieceView rook;
        [SerializeField] private PieceView knight;
        [SerializeField] private PieceView bishop;
        [SerializeField] private PieceView queen;
        [SerializeField] private PieceView king;

        [SerializeField]
        private float pieceHeight = 0.5f;

        public PieceView CreatePiece(
            PieceType type,
            PieceColor color,
            BoardTile tile)
        {
            PieceView prefab = GetPrefab(type);

            if (prefab == null || tile == null)
                return null;

            Vector3 spawnPosition = tile.transform.position;

            Quaternion rotation =
            color == PieceColor.White
                ? Quaternion.Euler(0f, 0f, 0f)
                : Quaternion.Euler(0f, 180f, 0f);

            PieceView piece = Instantiate(
                prefab,
                spawnPosition,
                rotation);
            
            piece.Initialize(type, color, tile);

            return piece;
        }

        private PieceView GetPrefab(PieceType type)
        {
            switch (type)
            {
                case PieceType.Pawn:
                    return pawn;

                case PieceType.Rook:
                    return rook;

                case PieceType.Knight:
                    return knight;

                case PieceType.Bishop:
                    return bishop;

                case PieceType.Queen:
                    return queen;

                case PieceType.King:
                    return king;

                default:
                    return null;
            }
        }
    }
}