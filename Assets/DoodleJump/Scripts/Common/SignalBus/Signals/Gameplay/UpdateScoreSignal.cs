namespace Assets.DoodleJump.Scripts.Common.SignalBus.Signals.Gameplay
{
    internal class UpdateScoreSignal
    {
        public int Score { get; private set; }
        public UpdateScoreSignal(int score)
        {
            Score = score;
        }
    }
}
