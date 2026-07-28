using UnityEngine;
using Chessmate.Core;

namespace Chessmate.Managers
{
    public class UIManager : IGameService
    {
        public void Initialize()
        {
            Debug.Log("UI Manager Initialized");
        }
    }
}