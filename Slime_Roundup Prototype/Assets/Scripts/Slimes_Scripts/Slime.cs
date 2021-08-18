using UnityEngine;

public class Slime : MonoBehaviour
{
    #region Variables

    [Header("Speed")]
    [SerializeField] private float idleSpeed = 5.0f;
    [SerializeField] private float avoidSpeed = 10.0f;
    [Header("Jump")]
    [SerializeField] private float idleJumpForce = 10;
    [SerializeField] private float avoidJumpForce = 5.0f;
    [Header("Rest")]
    [SerializeField] private float minRestTime;
    [SerializeField] private float maxRestTime;


    public bool IsLost { get; set; }  // On Set call Got Lost Event


    // Events Needed
    /* 
     * Slime Captured
     * Slime Got Lost
     */

    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

    private bool _isResting;
    private double _RestingProbability;
    private float _restingTime;
    private float _restingTimer;

    private Vector3 _lastMoveDir;
    private Rigidbody _slimeRb;
    
    
    #endregion

    private void Awake() {
        _slimeRb = GetComponent<Rigidbody>();
    }

    private void Start() {
        _isGrounded = true;
        _isResting = false;
        _RestingProbability = Random.Range(0,100);
    }

    void Update()
    {
        if(_isGrounded)
        {
            if(IsLost == false)
            {
                if (_isResting == false)
                {

                    if (WantToRest())
                    {
                        StartResting();
                    }
                    else
                    {
                        MoveTowardRandomDirection();
                    }
                }
                else
                {
                    Rest();
                }

            }
            else
            {
                MoveTowards( _lastMoveDir, avoidSpeed, avoidJumpForce);
            }
        }
    }

    

    public void AvoidTowardsDir(Vector3 moveDir) 
    {
        if (IsLost == false) 
        {
            MoveTowards(moveDir, avoidSpeed, avoidJumpForce);
        }
    }



    #region Slime Methods

    #region Resting Methods

    private bool WantToRest()
    {
        return Random.Range(0, 100) <= _RestingProbability;
    }

    private void StartResting()
    {
        _isResting = true;
        _restingTimer = 0;
        _restingTime = Random.Range(minRestTime, maxRestTime);
    }

    private void Rest()
    {
        _restingTimer += Time.deltaTime;
        if (_restingTimer >= _restingTime)
        {
            _isResting = false;
        }
    }

    #endregion

    #region Move Methods

    private void MoveTowards(Vector3 direction,float moveSpeed, float jumpForce)
    {
            _lastMoveDir = direction;

            _slimeRb.AddForce(Vector3.up * jumpForce );
            _slimeRb.AddForce(direction * moveSpeed );
            transform.LookAt(direction);
            _isGrounded = false;
    }

    private void MoveTowardRandomDirection()
    {
        Vector3 moveDir = RandomDirection();
        _lastMoveDir = moveDir;
        MoveTowards(moveDir, idleSpeed, idleJumpForce);
    }

    #endregion

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
