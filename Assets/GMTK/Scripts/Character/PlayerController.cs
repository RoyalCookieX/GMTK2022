using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<Vector2> _onMovement;
    [SerializeField] private UnityEvent<Vector2> _onLook;
    [SerializeField] private UnityEvent<bool> _onJump;
    [SerializeField] private UnityEvent<bool> _onShoot;

    [Header("Properties")]
    [SerializeField] private Vector2 _pitchYawSpeed = new Vector2(15f, 15f);

    private void OnMovement(InputValue value)
    {
        _onMovement.Invoke(value.Get<Vector2>());
    }

    private void OnLook(InputValue value)
    {
        _onLook?.Invoke(value.Get<Vector2>() * _pitchYawSpeed);
    }

    private void OnJump(InputValue value)
    {
        _onJump.Invoke(value.isPressed);
    }

    private void OnShoot(InputValue value)
    {
        _onShoot.Invoke(value.isPressed);
    }
}
