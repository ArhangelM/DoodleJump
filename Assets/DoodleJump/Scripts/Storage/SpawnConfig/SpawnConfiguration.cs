using UnityEngine;

namespace Assets.DoodleJump.Scripts.Storage.SpawnConfig
{
    [CreateAssetMenu(fileName = "SpawnConfiguration", menuName = "Scriptable Objects/SpawnConfiguration")]
    public class SpawnConfiguration : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _chance = 0.5f; 

        public GameObject Prefab => _prefab;
        public float Chance => _chance;
    }
}