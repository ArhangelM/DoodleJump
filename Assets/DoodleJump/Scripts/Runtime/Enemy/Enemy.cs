using Assets.DoodleJump.Scripts.Common;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Common;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Enemy
{
    public class Enemy : MonoBehaviour, IInteraction
    {
        [SerializeField] private LayerMask _destroyMask;
        public void Interaction()
        {
            SignalBus.Instance.Invoke(new EndGameSignal());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(Misc.IsInLayerMask(collision.gameObject, _destroyMask))
            {
                Destroy(gameObject);
            }
        }
    }
}