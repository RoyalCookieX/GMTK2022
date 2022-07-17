using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private Weapon _weapon;

    [Header("Properties")]
    [SerializeField] private float _yawMultiplier;
    [SerializeField] private float _pitchMultiplier;
    [SerializeField] private Vector2 _pitchRange = new Vector2(-70f, 70f);
    [SerializeField] private bool _isPaused;

    private Vector2 _yawPitch;
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Vector3 moveDirection = Quaternion.AngleAxis(_yawPitch.x, Vector3.up) * new Vector3(input.x, 0f, input.y);
        _characterMovement.SetMoveDirection(moveDirection);
    }

    private void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _yawPitch.x += input.x * _yawMultiplier;
        _yawPitch.y = Mathf.Clamp(_yawPitch.y + input.y * _pitchMultiplier, _pitchRange.x, _pitchRange.y);
        _playerCamera.SetYawPitch(_yawPitch);
    }

    private void OnJump(InputValue value)
    {
        bool input = value.isPressed;
        _characterMovement.SetJump(input);
    }

    private void OnShoot(InputValue value)
    {
        if (!value.isPressed)
            return;
        _weapon.OnShoot();
    }

    private void OnReload(InputValue value)
    {
        if (!value.isPressed)
            return;
        _weapon.OnReload();
    }

    private void OnPause(InputValue value)
    {
        if(_isPaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
