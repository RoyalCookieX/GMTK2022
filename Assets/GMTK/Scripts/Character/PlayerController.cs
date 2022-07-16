using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<Vector2> _onYawPitch;

 [Header("Components")]
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private PlayerCamera _playerCamera;

    [Header("Properties")]
    [SerializeField] private Vector2 _rotationSpeed = new Vector2(20.0f, 10.0f);

    private void OnMovement(InputValue value)
    {
        _characterMovement.SetMoveDirection(value.Get<Vector2>());
    }

    private void OnLook(InputValue value)
    {
        Vector2 yawPitch = value.Get<Vector2>() * _rotationSpeed;
        _characterMovement.AddYaw(yawPitch.x);
        _playerCamera.AddPitch(yawPitch.y);
        _onYawPitch?.Invoke(new Vector2(_characterMovement.Yaw, _playerCamera.Pitch));
    }

    private void OnJump(InputValue value)
    {
        bool isPressed = value.isPressed;
        _characterMovement.SetJump(isPressed);
    }

    private void OnShoot(InputValue value)
    {
        Debug.Log("Shoot!");
    }
}
