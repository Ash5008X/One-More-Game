using UnityEngine;
using Chessmate.Board.Rendering;
using Chessmate.Pieces;

namespace Chessmate.Gameplay
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Camera gameCamera;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                SelectionManager.Instance.DeselectCurrent();
                return;
            }

            BoardTile tile = hit.collider.GetComponent<BoardTile>();

            if (tile == null)
            {
                SelectionManager.Instance.DeselectCurrent();
                return;
            }

            if (SelectionManager.Instance.HasSelection())
            {
                PieceView selected = SelectionManager.Instance.SelectedPiece;

                if (tile.OccupyingPiece != null &&
                    tile.OccupyingPiece.Color == selected.Color)
                {
                    SelectionManager.Instance.SelectPiece(tile.OccupyingPiece);
                }
                else
                {
                    SelectionManager.Instance.TryMove(tile);
                }

                return;
            }

            if (tile.OccupyingPiece != null)
            {
                SelectionManager.Instance.SelectPiece(tile.OccupyingPiece);
            }
        }
    }
}