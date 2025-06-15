using Assets.DoodleJump.Scripts.Common.Enums;

namespace Assets.DoodleJump.Scripts.Common.SignalBus.Signals
{
    internal class PlayerMovingSignal
    {
        public Direction Direction { get; set; }

        public PlayerMovingSignal(Direction direction)
        {
            Direction = direction;
        }
    }
}
