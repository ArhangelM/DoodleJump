using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using System.Collections.Generic;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Player
{
    internal class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _shootForce = 20f;

        private Queue<GameObject> _clip = new(); 

        private void Awake()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity, _shootPoint);
                bullet.SetActive(false);
                _clip.Enqueue(bullet);
            }
        }

        private void OnEnable()
        {
            SignalBus.Instance.Subscribe<BulletRestoreSignal>(Restore);
            SignalBus.Instance.Subscribe<ShootSignal>(Shoot);
        }

        private void OnDisable()
        {
            SignalBus.Instance.Unsubscribe<BulletRestoreSignal>(Restore);
            SignalBus.Instance.Unsubscribe<ShootSignal>(Shoot);
        }

        public void Shoot(ShootSignal signal)
        {
            if (_bulletPrefab == null || _shootPoint == null)
            {
                Debug.LogError("Bullet prefab or shoot point is not assigned.");
                return;
            }

            GameObject bullet = _clip.Dequeue();
            bullet.SetActive(true);
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().linearVelocityX = _shootForce * -transform.localScale.x; 
        }

        private void Restore(BulletRestoreSignal signal)
        {
            signal.Bullet.SetActive(false);
            signal.Bullet.transform.position = _shootPoint.position;
            signal.Bullet.transform.SetParent(_shootPoint);
            signal.Bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            _clip.Enqueue(signal.Bullet);
        }
    }
}
