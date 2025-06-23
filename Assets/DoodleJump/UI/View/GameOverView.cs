using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Assets.DoodleJump.Scripts.Models;
using TMPro;
using Tools.Extensions;
using Tools.SignalBus;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DoodleJump.UI.View
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void RestartButtonClicked()
        {
            SignalBus.Instance.Invoke(new RestartGameSignal());
        }

        private void MenuButtonClicked()
        {
            SignalBus.Instance.Invoke(new OpenMenuSignal());
        }

        private void ShowScore(EndGameSignal signal)
        {
            _bestScoreText.text = Statistic.Instance.BestScore.ToString();
            _scoreText.text = Statistic.Instance.Score.ToString();
        }

        private void SubscribeEvents()
        {
            if(_restartButton.HasValue())
                _restartButton.onClick.AddListener(RestartButtonClicked);
            else
                Debug.LogError("Restart Button is not assigned in GameOverView.");
           
            if(_menuButton.HasValue())
                _menuButton.onClick.AddListener(MenuButtonClicked);
            else
                Debug.LogError("Menu Button is not assigned in GameOverView.");

            SignalBus.Instance.Subscribe<EndGameSignal>(ShowScore, 1);
        }

        private void UnsubscribeEvents()
        {
            if (_restartButton.HasValue())
                _restartButton.onClick.RemoveListener(RestartButtonClicked);
          
            if (_menuButton.HasValue())
                _menuButton.onClick.RemoveListener(MenuButtonClicked);

            SignalBus.Instance.Unsubscribe<EndGameSignal>(ShowScore);
        }
    }
}