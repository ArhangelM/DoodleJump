using UnityEngine;

namespace Assets.DoodleJump.Scripts.Common.SignalBus.Signals
{
    internal class BulletRestoreSignal
    {
        public GameObject Bullet { get; set; }

        public BulletRestoreSignal(GameObject bullet)
        {
            Bullet = bullet;
        }
    }
}
