namespace Chessmate.Core.Events
{
    public class MoveRequestedEvent
    {
        public string FromSquare { get; }
        public string ToSquare { get; }

        public MoveRequestedEvent(string fromSquare, string toSquare)
        {
            FromSquare = fromSquare;
            ToSquare = toSquare;
        }
    }
}