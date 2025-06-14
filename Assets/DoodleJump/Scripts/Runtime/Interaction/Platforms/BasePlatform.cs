using Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Platforms;
using UnityEngine;

public class BasePlatform : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _jumpForce = 3f;
    public float JumpForce => _jumpForce;

    public virtual void Interaction()
    {
        
    }

    public virtual void Spawn(GameObject prefab)
    {
        Instantiate(prefab, _spawnPoint.position, Quaternion.identity, _spawnPoint);
    }
}
