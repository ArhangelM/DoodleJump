using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.UI.View;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Player
{
    public class PlayerIventTrigger : MonoBehaviour
    {
        [SerializeField] private GameOverView _gameOverView;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EndZone"))
            {
                SignalBus.Instance.Invoke(new EndGameSignal());
            }
        }
    }
}