using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.Gameplay;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Player
{
    internal class ScoreCounter : MonoBehaviour
    {
        private int _maxHeight = 0;

        private void Update()
        {
            if(Time.timeScale == 0f)
                return;

            UpdateScore();
        }

        public void UpdateScore()
        {
            if (transform.position.y > _maxHeight)
            {
                _maxHeight = (int)transform.position.y;
                SignalBus.Instance.Invoke(new UpdateScoreSignal(_maxHeight));
            }

            if(transform.position.x <= 0)
                _maxHeight = 0;
        }
    }
}
