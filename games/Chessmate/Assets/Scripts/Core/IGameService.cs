namespace Chessmate.Core
{
    /// <summary>
    /// Every service in the game implements this interface.
    /// The Bootstrapper will initialize all services through it.
    /// </summary>
    public interface IGameService
    {
        void Initialize();
    }
}