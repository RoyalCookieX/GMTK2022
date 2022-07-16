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
    [SerializeField] private float _maxVelocity = 6f;
    [SerializeField] private float _moveSpeed = 30f;
    [SerializeField] private float _jumpStrength = 5f;
    [SerializeField, Range(0.01f, 1f)] private float _airControl = 0.35f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _groundCheckBegin = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _groundCheckEnd = new Vector3(0f, -1f, 0f);
    [SerializeField] private float _groundCheckRadius = 0.2f;

    public void SetMoveDirection(Vector2 localDirection)
    {
        MoveDirection = new Vector3(localDirection.x, 0f, localDirection.y) * _moveSpeed;
    }

    public void AddYaw(float yaw)
    {
        Yaw += yaw * Time.deltaTime;
        _rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * Yaw));
    }

    public void SetJump(bool jump)
    {
        IsJumping = jump;
    }

    private void OnEnable()
    {
        Yaw = transform.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        // check if character is grounded
        Vector3 begin = transform.TransformPoint(_groundCheckBegin);
        Vector3 end = transform.TransformPoint(_groundCheckEnd);
        Vector3 displacement = end - begin;
        Ray ray = new Ray(begin, displacement.normalized);
        IsGrounded = Physics.SphereCast(ray, _groundCheckRadius, displacement.magnitude, _groundMask);

        // set movement
        Vector3 moveDirection = MoveDirection;
        if (!IsGrounded)
            moveDirection *= _airControl;
        _rigidbody.AddRelativeForce(moveDirection, ForceMode.Force);

        // jump
        if(IsJumping && IsGrounded)
        {
            // zero y velocity
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            // jump
            _rigidbody.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
        }

        // clamp velocity
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.yellow;
        Vector3 begin = transform.TransformPoint(_groundCheckBegin);
        Vector3 end = transform.TransformPoint(_groundCheckEnd);
        Gizmos.DrawLine(begin, end);
        Gizmos.DrawWireSphere(end, _groundCheckRadius);
    }
}
