using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Compoents")]
    [SerializeField] private CharacterMovement _characterMovement;

    private void OnMovement(InputValue value)
    {
        _characterMovement.SetMoveDirection(value.Get<Vector2>());
    }

    private void OnLook(InputValue value)
    {
        Vector2 lookDelta = value.Get<Vector2>();
        _characterMovement.AddYaw(lookDelta.x);
    }

    private void OnJump(InputValue value)
    {
        Debug.Log("Jump!");
    }

    private void OnShoot(InputValue value)
    {
        Debug.Log("Shoot!");
    }
}
