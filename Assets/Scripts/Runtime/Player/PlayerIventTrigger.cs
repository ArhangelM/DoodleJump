using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIventTrigger : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private PlayerMovement _playerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EndZone"))
        {
            _gameOverView.ChangeViewStatus(true);
            _playerMovement.StopPlayer();
        }
    }
}
