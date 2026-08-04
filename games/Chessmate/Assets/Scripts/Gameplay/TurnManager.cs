using UnityEngine;
using Chessmate.Board.Data.Enums;

namespace Chessmate.Gameplay
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager Instance { get; private set; }

        public PieceColor CurrentTurn { get; private set; } = PieceColor.White;

        private void Awake()
        {
            
            Debug.Log("TurnManager Awake");

            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
        }

        public bool IsPlayersTurn(PieceColor color)
        {
            return color == CurrentTurn;
        }

        public void EndTurn()
        {
            CurrentTurn =
                CurrentTurn == PieceColor.White
                ? PieceColor.Black
                : PieceColor.White;

            Debug.Log("Turn : " + CurrentTurn);
        }

        public void ResetTurn()
        {
            CurrentTurn = PieceColor.White;
        }
        
    }
}