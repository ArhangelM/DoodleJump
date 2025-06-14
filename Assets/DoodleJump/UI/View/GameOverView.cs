using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Tools.SignalBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverObj;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartButtonClicked);  
        SignalBus.Instance.Subscribe<EndGameSignal>(ChangeViewStatus);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartButtonClicked);
        SignalBus.Instance.Unsubscribe<EndGameSignal>(ChangeViewStatus);
    }

    public void ChangeViewStatus(EndGameSignal signal)
    {
        _gameOverObj.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartButtonClicked()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneName);
    }
}
