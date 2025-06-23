using Tools.Extensions;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 0.01f;

        private Transform _target;
        private Vector3 _offset;

        private void LateUpdate()
        {
            if (_target.HasValue())
                CheckAndUpdateCameraPosition();
        }

        private void CheckAndUpdateCameraPosition()
        {
            if (transform.position.y > _target.position.y)
                return;

            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

            smoothedPosition = new Vector3(transform.position.x, smoothedPosition.y, -10f);
            transform.position = smoothedPosition;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            //_offset = transform.position - target.position;
        }
    }
}