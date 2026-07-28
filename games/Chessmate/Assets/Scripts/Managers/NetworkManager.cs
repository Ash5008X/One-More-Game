using UnityEngine;
using Chessmate.Core;

namespace Chessmate.Managers
{
    public class NetworkManager : IGameService
    {
        public void Initialize()
        {
            Debug.Log("Network Manager Initialized");
        }
    }
}