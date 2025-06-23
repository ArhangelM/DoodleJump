using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Tools.Extensions;
using Tools.SignalBus;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DoodleJump.UI.View
{
    internal class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }   

        private void OnStartButtonClicked()
        {
            SignalBus.Instance.Invoke(new StartGameSignal());
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void SubscribeEvents()
        {             
            if (_startButton.HasValue())
                _startButton.onClick.AddListener(OnStartButtonClicked);
            else
                Debug.LogError("Start Button is not assigned in MainMenu.");

            if (_exitButton.HasValue())
                _exitButton.onClick.AddListener(OnExitButtonClicked);
            else
                Debug.LogError("Exit Button is not assigned in MainMenu.");
        }

        private void UnsubscribeEvents()
        {
            if (_startButton.HasValue())
                _startButton.onClick.RemoveListener(OnStartButtonClicked);

            if (_exitButton.HasValue())
                _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }   
    }
}
