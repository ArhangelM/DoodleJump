using UnityEngine;

namespace Assets.DoodleJump.Scripts.Storage.GameConfig
{
    [CreateAssetMenu(fileName = "StartupConfiguration", menuName = "Scriptable Objects/StartupConfiguration")]
    public class StartupConfiguration : ScriptableObject
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public GameObject PlatformGeneratorPrefab { get; private set; }
        [field: SerializeField] public GameObject CameraPrefab { get; private set; }
    }
}