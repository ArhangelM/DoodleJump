using Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Platforms;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Interaction.Platforms
{
    public class BasePlatform : MonoBehaviour, ISpawner
    {
        [SerializeField] private Transform _spawnPoint;
        [field: SerializeField] public float JumpForce { get; private set; } = 3f;

        public virtual void Interaction()
        {

        }

        public virtual void Spawn(GameObject prefab)
        {
            Instantiate(prefab, _spawnPoint.position, Quaternion.identity, _spawnPoint);
        }
    }
}