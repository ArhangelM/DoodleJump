using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Tools.Extensions;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Managers
{
    internal class NavigationManager : MonoBehaviour
    {
        [SerializeField] private Canvas _endGameCanvas;
        [SerializeField] private Canvas _mainMenuCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private Canvas _background;

        private float _timeScaleStopGame = 0f;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            CheckCanvases();
            Initialization();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void GameOver(EndGameSignal signal)
        {
            _endGameCanvas.enabled = true;
            Time.timeScale = _timeScaleStopGame;
        }

        private void OpenMainMenu(OpenMenuSignal signal)
        {
            _endGameCanvas.enabled = false;
            _mainMenuCanvas.enabled = true;
            Time.timeScale = _timeScaleStopGame;
        }

        private void StartGame(StartGameSignal signal)
        {
            _gameCanvas.worldCamera = Camera.main;
            _mainMenuCanvas.enabled = false;
            _gameCanvas.enabled = true;
            _background.enabled = true;
        }

        private void RestartGame(RestartGameSignal signal)
        {
            _endGameCanvas.enabled = false;
        }

        private void CheckCanvases()
        {

            if (!_endGameCanvas.HasValue())
                Debug.LogError("End Game Canvas is not assigned in NavigationManager.");
            if (!_mainMenuCanvas.HasValue())
                Debug.LogError("Main Menu Canvas is not assigned in NavigationManager.");
            if (!_gameCanvas.HasValue())
                Debug.LogError("Game Canvas is not assigned in NavigationManager.");
            if (!_background.HasValue())
                Debug.LogError("Background Canvas is not assigned in NavigationManager.");
        }

        private void Initialization()
        {
            _endGameCanvas.enabled = false;
            _mainMenuCanvas.enabled = true;
            _gameCanvas.enabled = false;
            _background.enabled = false;
        }

        private void SubscribeEvents()
        {
            SignalBus.Instance.Subscribe<StartGameSignal>(StartGame);
            SignalBus.Instance.Subscribe<EndGameSignal>(GameOver);
            SignalBus.Instance.Subscribe<OpenMenuSignal>(OpenMainMenu);
            SignalBus.Instance.Subscribe<RestartGameSignal>(RestartGame);
        }

        private void UnsubscribeEvents()
        {
            SignalBus.Instance.Unsubscribe<StartGameSignal>(StartGame);
            SignalBus.Instance.Unsubscribe<EndGameSignal>(GameOver);
            SignalBus.Instance.Unsubscribe<OpenMenuSignal>(OpenMainMenu);
            SignalBus.Instance.Unsubscribe<RestartGameSignal>(RestartGame);
        }
    }
}
