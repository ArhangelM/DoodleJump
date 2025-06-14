using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Tools.SignalBus;
using UnityEngine;

public class PlayerIventTrigger : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private PlayerMovement _playerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EndZone"))
        {
            SignalBus.Instance.Invoke(new EndGameSignal());
            _playerMovement.StopPlayer();
        }
    }
}
