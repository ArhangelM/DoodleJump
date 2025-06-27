using Assets.DoodleJump.Scripts.Common.Helpers;
using Assets.DoodleJump.Scripts.Storage.GameConfig;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.DoodleJump.Scripts.Runtime.Settings
{
    public class Setup : MonoBehaviour
    {
        private async void Start()
        {
            try
            {
                var configTask = LoadConfiguration();
                var prefabsTask = LoadPrefabs();

                await UniTask.WhenAll(configTask, prefabsTask);
                await UniTask.Delay(10000);
                NextScene();
            }
            catch (Exception ex)
            {
                ErrorShow(ex);
            }
        }

        private async UniTask LoadConfiguration()
        {
            await AddressableHelper.LoadAddressableAsset<StartupConfiguration>("StartupConfiguration");
        }

        private async UniTask LoadPrefabs()
        {
            var playerTask = AddressableHelper.LoadAddressableAsset<GameObject>("Player");
            var cameraTask = AddressableHelper.LoadAddressableAsset<GameObject>("Main Camera");
            var platformGeneratorTask = AddressableHelper.LoadAddressableAsset<GameObject>("LocationGenerator");

            await UniTask.WhenAll(playerTask, cameraTask, platformGeneratorTask);
        }

        private void ErrorShow(Exception ex)
        {
            Debug.LogError($"Error during loading: {ex.Message}");
        }

        private void NextScene()
        {
            Debug.Log("All configurations and prefabs loaded successfully.");
            SceneManager.LoadScene(1);
        }
    }
}