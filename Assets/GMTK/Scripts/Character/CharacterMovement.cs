using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 MoveDirection { get; private set; }
    public float Yaw { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsGrounded { get; private set; }

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Properties")]
    [SerializeField] private float _moveSpeed = 30f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private float _jumpGravity = 10f;
    [SerializeField, Range(0.01f, 1f)] private float _airControl = 0.35f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _groundCheckStart = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _groundCheckEnd = new Vector3(0f, -1f, 0f);
    [SerializeField] private float _groundCheckRadius = 0.2f;

    public void SetMoveDirection(Vector3 moveDirection)
    {
        moveDirection.y = 0f;
        MoveDirection = moveDirection.normalized;
    }

    public void SetJump(bool jump)
    {
        IsJumping = jump;
    }

    private void CheckGrounded()
    {
        Vector3 start = transform.TransformPoint(_groundCheckStart);
        Vector3 end = transform.TransformPoint(_groundCheckEnd);
        Vector3 diff = end - start;

        IsGrounded = Physics.SphereCast(start, _groundCheckRadius, diff.normalized, out RaycastHit _, diff.magnitude, _groundMask);
    }

    public void FixedUpdate()
    {
        CheckGrounded();

        Vector3 targetVelocity = MoveDirection * _moveSpeed;
        Vector3 velocityDiff = targetVelocity - _rigidbody.velocity;
        velocityDiff.y = 0f;

        float control = IsGrounded ? 1f : _airControl;
        Vector3 acceleration = velocityDiff * (_acceleration * control);
        acceleration += Vector3.down * _jumpGravity;
        _rigidbody.AddForce(acceleration);

        if (!IsJumping || !IsGrounded)
            return;

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        float jumpVelocity = Mathf.Sqrt(_jumpGravity * _jumpHeight * 2f);
        Vector3 velocity = _rigidbody.velocity;
        velocity.y = jumpVelocity;
        _rigidbody.velocity = velocity;
    }

    private void OnDrawGizmosSelected()
    {
        CheckGrounded();

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Vector3 start = transform.TransformPoint(_groundCheckStart);
        Vector3 end = transform.TransformPoint(_groundCheckEnd);
        Gizmos.DrawWireSphere(start, _groundCheckRadius);
        Gizmos.DrawWireSphere(end, _groundCheckRadius);
        Gizmos.DrawLine(start, end);
    }
}
