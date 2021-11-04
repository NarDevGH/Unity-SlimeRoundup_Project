using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Slime_PhysicsBasedMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float _idleMoveSpeed = 5.0f;
    [SerializeField] private float _avoidMoveSpeed = 10.0f;
    [Space]
    [SerializeField] private float _idleJumpForce = 10;
    [SerializeField] private float _avoidJumpForce = 5.0f;

    private float _moveSpeedModifier;
    private float _jumpForceModifier;
    #region Modifiers Accesors
    public float MoveSpeedModifier 
    { 
        get { return _moveSpeedModifier; }
        set { _moveSpeedModifier = value; }
    }
    public float JumpForceModifier 
    {
        get { return _jumpForceModifier; }
        set { _jumpForceModifier = value; }
    }
    #endregion


    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }


    private Vector3 _lastMoveDir;
    public Vector3 LastMoveDirection { get { return _lastMoveDir; } }

    
    private Rigidbody _slimeRb;
    
    #endregion

    private void Awake() {
        _slimeRb = GetComponent<Rigidbody>();
    }

    private void Start() {
        _slimeRb.freezeRotation = true;
        _lastMoveDir = Vector3.zero;
        _isGrounded = true;
        _moveSpeedModifier = _jumpForceModifier = 1f;
    }



    public void MoveTowardsDir(Vector3 moveDir) 
    {
        MoveTowards(moveDir, _idleMoveSpeed, _idleJumpForce);
    }

    public void AvoidTowardsDir(Vector3 moveDir) 
    {
        MoveTowards(moveDir, _avoidMoveSpeed, _avoidJumpForce);
    }

    public void MoveTowardRandomDirection()
    {
        Vector3 moveDir = RandomDirection();
        MoveTowards(moveDir, _idleMoveSpeed, _idleJumpForce);
    }

    #region Movement Related Methods

    private void MoveTowards(Vector3 direction,float moveSpeed, float jumpForce)
    {
        if (_isGrounded) 
        {
            _isGrounded = false;

            moveSpeed *= MoveSpeedModifier;
            jumpForce *= JumpForceModifier;

            _slimeRb.AddForce(Vector3.up * jumpForce );
            _slimeRb.AddForce(direction * moveSpeed );
            transform.LookAt(direction);
            print(1);
            _lastMoveDir = direction;
        }
    }

    

    private Vector3 RandomDirection()
    {
        int xdir = Random.Range(-1,1);
        int zdir = Random.Range(-1,1);
        return new Vector3(xdir,0,zdir);
    }

    #endregion


    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground"))
            _isGrounded = true;
    }
}
