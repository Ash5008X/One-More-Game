using System.Collections.Generic;
using UnityEngine;
using Chessmate.Board.Rendering;

namespace Chessmate.Gameplay
{
    public class HighlightManager : MonoBehaviour
    {
        public static HighlightManager Instance { get; private set; }

        [Header("Highlight Materials")]
        [SerializeField] private Material selectedTileMaterial;
        [SerializeField] private Material captureTileMaterial;
        [SerializeField] private Material lastMoveFromMaterial;
        [SerializeField] private Material lastMoveToMaterial;
        [SerializeField] private Material checkTileMaterial;

        private readonly List<BoardTile> moveHighlights = new();

        private BoardTile selectedTile;

        private BoardTile lastMoveFrom;
        private BoardTile lastMoveTo;

        private BoardTile checkedKingTile;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        //==================================================
        // LEGAL MOVES
        //==================================================

        public void ShowMoves(List<BoardTile> moves)
        {
            ClearMoveHighlights();

            foreach (BoardTile tile in moves)
            {
                if (tile.OccupyingPiece == null)
                {
                    tile.ShowMoveDot();
                }
                else
                {
                    tile.ShowHighlight(captureTileMaterial);
                }

                moveHighlights.Add(tile);
            }
        }

        public void ClearMoveHighlights()
        {
            foreach (BoardTile tile in moveHighlights)
            {
                if (tile == null)
                    continue;

                tile.HideMoveDot();

                // Only hide capture highlights.
                if (tile.OccupyingPiece != null)
                    tile.HideHighlight();
            }

            moveHighlights.Clear();
        }

        //==================================================
        // SELECTED TILE
        //==================================================

        public void ShowSelectedTile(BoardTile tile)
        {
            if (selectedTile != null)
                selectedTile.HideHighlight();

            selectedTile = tile;

            if (selectedTile != null)
                selectedTile.ShowHighlight(selectedTileMaterial);
        }

        public void ClearSelectedTile()
        {
            if (selectedTile != null &&
                selectedTile != lastMoveFrom &&
                selectedTile != lastMoveTo)
            {
                selectedTile.HideHighlight();
            }

            selectedTile = null;
        }

        //==================================================
        // LAST MOVE
        //==================================================

        public void ShowLastMove(BoardTile from, BoardTile to)
        {
            ClearLastMove();

            lastMoveFrom = from;
            lastMoveTo = to;

            if (lastMoveFrom != null)
                lastMoveFrom.ShowHighlight(lastMoveFromMaterial);

            if (lastMoveTo != null)
                lastMoveTo.ShowHighlight(lastMoveToMaterial);
        }

        public void ClearLastMove()
        {
            if (lastMoveFrom != null)
                lastMoveFrom.HideHighlight();

            if (lastMoveTo != null)
                lastMoveTo.HideHighlight();

            lastMoveFrom = null;
            lastMoveTo = null;
        }

        //==================================================
        // CHECK
        //==================================================

        public void ShowCheck(BoardTile kingTile)
        {
            ClearCheck();

            checkedKingTile = kingTile;

            if (checkedKingTile != null)
                checkedKingTile.ShowHighlight(checkTileMaterial);
        }

        public void ClearCheck()
        {
            if (checkedKingTile != null)
                checkedKingTile.HideHighlight();

            checkedKingTile = null;
        }

        //==================================================
        // CLEAR EVERYTHING
        //==================================================

        public void ClearAll()
        {
            ClearMoveHighlights();
            ClearSelectedTile();
            ClearLastMove();
            ClearCheck();
        }
    }
}