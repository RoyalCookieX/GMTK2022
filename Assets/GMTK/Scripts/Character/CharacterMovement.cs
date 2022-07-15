using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 MoveDirection { get; private set; }
    public float Yaw { get; private set; }

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    [Header("Properties")]
    [SerializeField] private float _moveSpeed = 50;
    [SerializeField] private float _yawSpeed = 50;

    public void SetMoveDirection(Vector2 localDirection)
    {
        MoveDirection = new Vector3(localDirection.x, 0f, localDirection.y) * _moveSpeed;
    }

    public void AddYaw(float yaw)
    {
        Yaw += yaw * _yawSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(MoveDirection, ForceMode.Acceleration);
        _rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * Yaw * Time.deltaTime));
    }
}
