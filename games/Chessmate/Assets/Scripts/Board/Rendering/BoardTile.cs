using UnityEngine;

namespace Chessmate.Board.Rendering
{
    public class BoardTile : MonoBehaviour
    {
        private Renderer tileRenderer;

        public int Row { get; private set; }

        public int Column { get; private set; }

        private void Awake()
        {
            tileRenderer = GetComponent<Renderer>();
        }

        public void Initialize(int row, int column, Material material)
        {
            Row = row;
            Column = column;

            SetMaterial(material);
        }

        public void SetMaterial(Material material)
        {
            tileRenderer.sharedMaterial = material;
        }
    }
}