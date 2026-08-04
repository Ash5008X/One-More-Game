using UnityEngine;
using Chessmate.Board.Data.Enums;

namespace Chessmate.Pieces
{
    public class PieceRenderer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private Material whitePiece;

        [SerializeField] private Material blackPiece;

        public void SetColor(PieceColor color)
        {
            if (meshRenderer == null)
            {
                Debug.LogError("MeshRenderer is missing on " + gameObject.name);
                return;
            }

            meshRenderer.material =
                color == PieceColor.White
                ? whitePiece
                : blackPiece;
        }
    }
}