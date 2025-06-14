using Assets.DoodleJump.Scripts.Runtime.Interfaces.Actions.Common;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _humpForce = 5f;

    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerInputSystem _playerInputSystem;
    private Vector2 _moveInput;
    private bool _isLeft;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        _playerInputSystem.Enable();
        _playerInputSystem.Player.ScreenTouched.performed += OnMovePerformed();
        _playerInputSystem.Player.ScreenTouched.canceled += OnMoveCanceled();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.bodyType == RigidbodyType2D.Static)
            return;

        MovePlayer();
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
        _playerInputSystem.Player.ScreenTouched.performed -= OnMovePerformed();
        _playerInputSystem.Player.ScreenTouched.canceled -= OnMoveCanceled();
    }

    private Action<InputAction.CallbackContext> OnMoveCanceled()
    {
        return (InputAction.CallbackContext param) => _moveInput = Vector2.zero;
    
    }

    private Action<InputAction.CallbackContext> OnMovePerformed()
    {
        return (InputAction.CallbackContext param) =>
        {
            var rawInput = _playerInputSystem.Player.Move.ReadValue<Vector2>();
            _moveInput = CalculateTouchInput(rawInput);

            _isLeft = _moveInput.x < 0;
            transform.localScale = new Vector3(_isLeft ? 1 : -1, transform.localScale.y, transform.localScale.z);
        };
    }
       
    public void StopPlayer()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void MovePlayer()
    {
        var targetVelocity = new Vector2(_moveInput.x * _moveSpeed, _rigidbody.linearVelocity.y);
        _rigidbody.linearVelocity = targetVelocity;
    }

     private Vector2 CalculateTouchInput(Vector2 rawTouchPosition)
     {
         const float screenSplitFactor = 0.5f;
         if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
         {
             float screenWidth = Screen.width;
             if(rawTouchPosition.x < screenWidth * screenSplitFactor)
             {
                 return new Vector2(-1f, 0f);
             }

             return new Vector2(1f, 0f);
         }

         return Vector2.zero;
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
}
