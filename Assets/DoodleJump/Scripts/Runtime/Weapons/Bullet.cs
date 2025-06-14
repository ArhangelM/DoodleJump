using Assets.DoodleJump.Scripts.Common;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using System.Collections.Generic;
using System.Linq;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Weapons
{
    internal class Bullet : MonoBehaviour
    {
        [SerializeField] private List<LayerMask> _colisionMasks;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (var mask in _colisionMasks)
            {
                if (Misc.IsInLayerMask(collision.gameObject, mask))
                {
                    SignalBus.Instance.Invoke(new BulletRestoreSignal(gameObject));
                    return;
                }
            }
        }
    }
}
