using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.01f;

    private Vector3 _offset;

    private void LateUpdate()
    {
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

}
