using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Slime_PhysicsBasedMovement : MonoBehaviour,ISlimeMovement
{
    #region CONST_VALUES
    private const float CONST_SPEED_TOWARDS_UP = 0.1f;
    private const float CONST_SPEED_TOWARDS_DIR = 1f;

    private const float CONST_JUMPFORCE_TOWARDS_UP = 1f;
    private const float CONST_JUMPFORCE_TOWARDS_DIR = 0.1f;

    private const float CONST_GROUNDCHECK_DIST = .1f;
    #endregion


    public float JumpCooldown { get; set; } = 0.25f;
    public Rigidbody SlimeRigidbody { get; private set; }

    public bool IsGrounded => IsGroundedFunc();


    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;

    private void Awake()
    {
        SlimeRigidbody = GetComponent<Rigidbody>();
        SlimeRigidbody.freezeRotation = true;
    }

    public void StopMovementRoutines() 
    {
        StopAllCoroutines();
    }

    public IEnumerator GoTowards(Vector3 direction, float speed, float jumpForce, int jumps=1)
    {
        transform.localRotation = Quaternion.LookRotation(direction);

        for (int i = 0; i < jumps; i++)
        {
            var jumpRoutine = StartCoroutine(JumpTowards(direction, speed, jumpForce));
            yield return jumpRoutine;

            // If already perform all jumps, dont wait CONST_JUMP_COOLDOWN
            if (i < jumps - 1) yield return new WaitForSeconds(JumpCooldown);
        }

        StopMovementRoutines();
    }


    private Vector3 RandomDirection() 
    {
        float x = Random.Range(-1.0f, 1.0f);

        float z = Random.Range(-1.0f, 1.0f);

        return new Vector3(x, 0, z);
    }

    public IEnumerator GoTowardsRandomDirection(float speed, float jumpForce, int jumps = 1)
    {
        Vector3 direction = RandomDirection();
        transform.localRotation = Quaternion.LookRotation(direction);
        for (int i = 0; i < jumps; i++)
        {
            var jumpRoutine = StartCoroutine(JumpTowards(direction, speed, jumpForce));
            yield return jumpRoutine;

            // If already perform all jumps, dont wait JumpCooldown
            if (i < jumps-1) yield return new WaitForSeconds(JumpCooldown);
        }

        StopMovementRoutines();
    }

    public IEnumerator JumpTowards(Vector3 direction, float jumpForce)
    {
        if (SlimeRigidbody is null) yield break;

        var forceTowardsUp = jumpForce * CONST_JUMPFORCE_TOWARDS_UP;
        var forceTowardsDir = jumpForce * CONST_JUMPFORCE_TOWARDS_DIR;

        Vector3 finalVect = direction * forceTowardsDir;
        finalVect.y = forceTowardsUp;

        SlimeRigidbody.AddForce(finalVect, ForceMode.Impulse);

        //addforce will be aplied the next frame
        yield return null;

        //wait until it start falling
        yield return new WaitUntil(() => SlimeRigidbody.velocity.y <= 0);

        //wait until it hits the ground again
        yield return new WaitUntil(() => IsGrounded);

        SlimeRigidbody.velocity = Vector3.zero;
    }

    public IEnumerator JumpTowards(Vector3 direction, float speed, float jumpForce)
    {
        if (SlimeRigidbody is null) yield break; 

        direction *= speed * CONST_SPEED_TOWARDS_DIR;
        jumpForce *= speed > 1 ? Mathf.Pow(jumpForce, 1 / speed * CONST_SPEED_TOWARDS_UP) : jumpForce;

        var forceTowardsUp = jumpForce * CONST_JUMPFORCE_TOWARDS_UP;
        var forceTowardsDir = jumpForce * CONST_JUMPFORCE_TOWARDS_DIR;

        Vector3 finalVect = direction * forceTowardsDir;
        finalVect.y = forceTowardsUp;

        SlimeRigidbody.AddForce(finalVect,ForceMode.Impulse);

        //addforce will be aplied the next frame
        yield return null;

        //wait until it start falling
        yield return new WaitUntil(() => SlimeRigidbody.velocity.y <= 0);

        //wait until it hits the ground again
        yield return new WaitUntil(() => IsGrounded);

        SlimeRigidbody.velocity = Vector3.zero;
    }


    private bool IsGroundedFunc() 
    {
        return Physics.Raycast(transform.position, Vector3.down,CONST_GROUNDCHECK_DIST,groundLayer);
    }
}
