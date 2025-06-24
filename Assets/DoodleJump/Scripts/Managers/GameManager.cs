using Assets.DoodleJump.Scripts.Common.Helpers;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Assets.DoodleJump.Scripts.Runtime.Location;
using Assets.DoodleJump.Scripts.Runtime.Player;
using Assets.DoodleJump.Scripts.Storage.GameConfig;
using Cysharp.Threading.Tasks;
using Tools.Extensions;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Managers
{
    internal class GameManager : MonoBehaviour
    {
        private StartupConfiguration _configuration;
        private GameObject _player;
        private GameObject _platformGenerator;
        private GameObject _camera;

        private LocationGenerator _locationGenerator;

        private float _timeScaleInGame = 1f; 
        private float _timeScaleStopGame = 0f;

        private async void Awake()
        {
            FileHelper.LoadStatistic();
            await LoadConfigurations();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private async void Start()
        {
            Time.timeScale = _timeScaleStopGame;
            await LoadPrefabs();
            GenerateObjects();    
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private async UniTask LoadConfigurations()
        {
            _configuration = await AddressableHelper.LoadAddressableAsset<StartupConfiguration>("StartupConfiguration");
            if (_configuration == null)
                Debug.LogError("StartupConfiguration not found.");
            else
                Debug.Log("StartupConfiguration loaded successfully.");
        }

        private async UniTask LoadPrefabs()
        {
            if (!_player.HasValue())
                _player = await AddressableHelper.LoadAddressableAsset<GameObject>("Player");

            if (!_camera.HasValue())
                _camera = await AddressableHelper.LoadAddressableAsset<GameObject>("Main Camera");

            if (!_platformGenerator.HasValue())
                _platformGenerator = await AddressableHelper.LoadAddressableAsset<GameObject>("LocationGenerator");

            Debug.Log("Loading prefabs from Addressables...");
        }

        private void GenerateObjects()
        {
            if (_player.HasValue())
                _player = Instantiate(_configuration.PlayerPrefab, Vector3.zero, Quaternion.identity);

            if(_camera.HasValue())
                _camera = Instantiate(_configuration.CameraPrefab, Vector3.zero, Quaternion.identity);

            if (_platformGenerator.HasValue())
                _platformGenerator = Instantiate(_configuration.PlatformGeneratorPrefab);
   
            if (!_platformGenerator.TryGetComponent(out _locationGenerator))
                Debug.LogError("LocationGenerator component not found on PlatformGenerator prefab.");

            _locationGenerator.DisplaySetup();
        }

        private void SetupComponents()
        {
            _camera.GetComponent<CameraFollow>().SetTarget(_player.transform);

            if(_platformGenerator.HasValue())
            { 
                _locationGenerator.SetPlayerTransform(_player.transform);
            }
        }

        private void StartGame(StartGameSignal signal)
        {
            SetupComponents();
            SetupScene();
            Time.timeScale = _timeScaleInGame;
        }

        private void RestartGame(RestartGameSignal signal)
        {
            SetupScene();
            Time.timeScale = _timeScaleInGame;
        }

        private void SetupScene()
        {
            if (_player.HasValue())
                _player.transform.position = Vector3.zero;
            if (_platformGenerator.HasValue())
                _locationGenerator.ResetGenerator();
            if (_camera.HasValue())
                _camera.transform.position = Vector3.zero;
        }

        private void EndGame(EndGameSignal signal)
        {
            FileHelper.SaveStatistic();
        }

        private void SubscribeEvents()
        {
            SignalBus.Instance.Subscribe<StartGameSignal>(StartGame);
            SignalBus.Instance.Subscribe<RestartGameSignal>(RestartGame);
            SignalBus.Instance.Subscribe<EndGameSignal>(EndGame, 2);
        }

        private void UnsubscribeEvents()
        {
            SignalBus.Instance.Unsubscribe<StartGameSignal>(StartGame);
            SignalBus.Instance.Unsubscribe<RestartGameSignal>(RestartGame);
            SignalBus.Instance.Unsubscribe<EndGameSignal>(EndGame);
        }
    }
}
