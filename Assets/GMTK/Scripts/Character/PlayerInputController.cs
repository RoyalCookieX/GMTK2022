using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private void OnMovement(InputValue value)
    {
        
    }

    private void OnLook(InputValue value)
    {

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
