using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Platforms
{
    public interface ISpawner
    {
        void Spawn(GameObject prefab);
    }
}