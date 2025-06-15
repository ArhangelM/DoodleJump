using UnityEngine;

namespace Assets.DoodleJump.Scripts.Storage.SpawnConfig
{
    [CreateAssetMenu(fileName = "SpawnConfiguration", menuName = "Scriptable Objects/SpawnConfiguration")]
    public class SpawnConfiguration : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Chance { get; private set; } = 0.5f; 
    }
}