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
    }

    private void Start()
    {
        ChangeViewStatus(false);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartButtonClicked);
    }

    public void ChangeViewStatus(bool isActive)
    {
        _gameOverObj.SetActive(isActive);
    }

    private void RestartButtonClicked()
    {
        string currentSceneName  = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
