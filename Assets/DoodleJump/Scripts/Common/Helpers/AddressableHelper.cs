using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.DoodleJump.Scripts.Common.Helpers
{
    public static class AddressableHelper
    {
        public static async UniTask<T> LoadAddressableAsset<T>(string key)
        {
            return await Addressables.LoadAssetAsync<T>(key);
        }
    }
}
