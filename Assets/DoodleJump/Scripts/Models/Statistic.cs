namespace Assets.DoodleJump.Scripts.Models
{
    public class Statistic
    {
        private static Statistic _instance;
        public int StarCount { get; private set; }
        public int EnemyCount { get; private set; }
        public int Score { get; private set; }
        public int BestScore { get; private set; }

        public static Statistic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Statistic();
                return _instance;
            }
        }

        private Statistic()
        {

        }

        public void Reset()
        {
            StarCount = 0;
            EnemyCount = 0;
            Score = 0;
        }

        public void AddStar() => StarCount++;

        public void KillEnemy() => EnemyCount++;

        public void UpdateScore(int value)
        {
            if(Score < value) 
                Score = value;
        }

        public void UpdateBestScore(int value)
        {
            if (BestScore < value)
                BestScore = value;
        }
    }
}
