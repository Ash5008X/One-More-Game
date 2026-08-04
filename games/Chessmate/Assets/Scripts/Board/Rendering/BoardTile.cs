using UnityEngine;
using Chessmate.Pieces;

namespace Chessmate.Board.Rendering
{
    public class BoardTile : MonoBehaviour
    {
        private Renderer tileRenderer;

        [Header("Prefabs")]
        [SerializeField] private GameObject moveDotPrefab;
        [SerializeField] private GameObject tileHighlightPrefab;

        private GameObject moveDot;
        private GameObject tileHighlight;

        private MeshRenderer highlightRenderer;

        private Material defaultMaterial;

        public int Row { get; private set; }
        public int Column { get; private set; }

        public PieceView OccupyingPiece { get; private set; }

        private void Awake()
        {
            tileRenderer = GetComponent<Renderer>();
        }

        public void Initialize(int row, int column, Material tileMaterial)
        {
            Row = row;
            Column = column;

            defaultMaterial = tileMaterial;
            tileRenderer.material = tileMaterial;

            CreateMoveDot();
            CreateHighlight();
        }

        private void CreateMoveDot()
        {
            if (moveDotPrefab == null)
                return;

            moveDot = Instantiate(moveDotPrefab, transform);

            moveDot.transform.localPosition = new Vector3(0f, 0.02f, 0f);
            moveDot.SetActive(false);
        }

        private void CreateHighlight()
        {
            if (tileHighlightPrefab == null)
                return;

            tileHighlight = Instantiate(tileHighlightPrefab, transform);

            tileHighlight.transform.localPosition = new Vector3(0f, 0.01f, 0f);
            tileHighlight.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            // Board tile is a Plane scaled to 0.1.
            // Quad is 1x1, so multiply by 10.
            tileHighlight.transform.localScale = Vector3.one * 10f;

            highlightRenderer = tileHighlight.GetComponent<MeshRenderer>();

            tileHighlight.SetActive(false);
        }

        //==================================================
        // MOVE DOT
        //==================================================

        public void ShowMoveDot()
        {
            if (moveDot != null)
                moveDot.SetActive(true);
        }

        public void HideMoveDot()
        {
            if (moveDot != null)
                moveDot.SetActive(false);
        }

        //==================================================
        // TILE HIGHLIGHT
        //==================================================

        public void ShowHighlight(Material material)
        {
            if (tileHighlight == null || highlightRenderer == null)
                return;

            highlightRenderer.material = material;
            tileHighlight.SetActive(true);
        }

        public void HideHighlight()
        {
            if (tileHighlight != null)
                tileHighlight.SetActive(false);
        }

        //==================================================
        // TILE MATERIAL
        //==================================================

        public void SetMaterial(Material material)
        {
            tileRenderer.material = material;
        }

        public void ResetMaterial()
        {
            tileRenderer.material = defaultMaterial;
        }

        //==================================================
        // PIECE
        //==================================================

        public void SetOccupyingPiece(PieceView piece)
        {
            OccupyingPiece = piece;
        }

        public void ClearPiece()
        {
            OccupyingPiece = null;
        }
    }
}