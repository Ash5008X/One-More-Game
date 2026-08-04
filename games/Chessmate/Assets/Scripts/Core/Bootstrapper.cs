using System.Collections.Generic;
using UnityEngine;
using Chessmate.Managers;
using Chessmate.Board.Data.Models;
using Chessmate.Board.Rendering;

namespace Chessmate.Core
{
    /// <summary>
    /// Entry point of the Chessmate client.
    /// Responsible for creating and initializing services.
    /// </summary>
    public class Bootstrapper : MonoBehaviour
    {
        private readonly List<IGameService> services = new();

        private void RegisterServices()
        {
            var networkManager = new NetworkManager();
            var boardManager = new BoardManager();
            var uiManager = new UIManager();

            ServiceRegistry.Register(networkManager);
            ServiceRegistry.Register(boardManager);
            ServiceRegistry.Register(uiManager);

            services.Add(networkManager);
            services.Add(boardManager);
            services.Add(uiManager);
        }

        private void InitializeServices()
        {
            foreach (var service in services)
            {
                service.Initialize();
            }
        }

        private void TestBoard()
        {
            BoardManager boardManager =
                ServiceRegistry.Get<BoardManager>();

            Square square = boardManager.GetSquare(0, 0);

            Debug.Log(
                $"Square Created -> Row: {square.Row}, Column: {square.Column}"
            );
        }

        private void TestFEN()
        {
            BoardRenderer boardRenderer =
                FindAnyObjectByType<BoardRenderer>();

            boardRenderer.GenerateBoard();

            BoardManager boardManager =
                ServiceRegistry.Get<BoardManager>();

            boardManager.LoadPosition(
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR"
            );

            boardRenderer.RenderPosition(boardManager.Board);

            Debug.Log("FEN parsed successfully.");
        }
        
        private void Awake()
        {
            Debug.Log("=== Chessmate Bootstrap Starting ===");

            RegisterServices();

            InitializeServices();

        #if UNITY_EDITOR
            TestFEN();
        #endif

            Debug.Log("=== Chessmate Ready ===");
        }
    }
}