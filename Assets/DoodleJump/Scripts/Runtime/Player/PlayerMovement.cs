using Assets.DoodleJump.Scripts.Common.Enums;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals;
using Assets.DoodleJump.Scripts.Common.SignalBus.Signals.UI;
using Assets.DoodleJump.Scripts.Runtime.Interaction.Platforms;
using Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Common;
using Tools.SignalBus;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _moveInput;
        private bool _isLeft;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            if (Input.touchCount < 1)
            {
                _moveInput = Vector2.zero;
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IInteraction>(out var obj))
            {
                obj.Interaction();
            }

            if (collision.gameObject.TryGetComponent<BasePlatform>(out var platform))
            {
                if (_rigidbody.linearVelocity.y > 0)
                    return;

                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, platform.JumpForce);
                platform.Interaction();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IInteraction>(out var obj))
            {
                obj.Interaction();
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void MovePlayer()
        {
            var targetVelocity = new Vector2(_moveInput.x * _moveSpeed, _rigidbody.linearVelocity.y);
            _rigidbody.linearVelocity = targetVelocity;
        }

        private void SetDirection(PlayerMovingSignal signal)
        {
            Debug.Log($"PlayerMovingSignal: {signal.Direction}");
            _moveInput = signal.Direction switch
            {
                Direction.Left => new Vector2(-1f, 0f),
                Direction.Right => new Vector2(1f, 0f),
                _ => Vector2.zero
            };

            _isLeft = _moveInput.x < 0;
            transform.localScale = new Vector3(_isLeft ? 1 : -1, transform.localScale.y, transform.localScale.z);
        }

        private void ResetVelocity(RestartGameSignal signal)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void SubscribeEvents()
        {
            SignalBus.Instance.Subscribe<PlayerMovingSignal>(SetDirection);
            SignalBus.Instance.Subscribe<RestartGameSignal>(ResetVelocity);
        }

        private void UnsubscribeEvents()
        {
            SignalBus.Instance.Unsubscribe<PlayerMovingSignal>(SetDirection);
            SignalBus.Instance.Unsubscribe<RestartGameSignal>(ResetVelocity);
        }
    }
}