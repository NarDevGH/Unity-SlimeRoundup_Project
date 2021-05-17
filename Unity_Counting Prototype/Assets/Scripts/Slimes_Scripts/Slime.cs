using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float idleSpeed = 5.0f;
    [SerializeField] private float avoidSpeed = 10.0f;
    [SerializeField] private float idleJumpForce = 10;
    [SerializeField] private float avoidJumpForce = 5.0f;
    [SerializeField] private float avoidPlayerDistance = 2.0f;


    private bool isOnGround;
    public bool isLost = false;
    private bool isChilling;
    private float chillProbability;

    private Vector3 lastMoveDir;
    private Rigidbody slimeRb;
    private void Awake() {
        slimeRb = GetComponent<Rigidbody>();
    }

    private void Start() {
        isOnGround = true;
        isChilling = false;
        chillProbability = Random.Range(0,100);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround){
            if(isLost == false){

                float distaneFromPlayer = (PlayerMousePosition.mousePositionIn3dSpace - transform.position).magnitude;

                if( distaneFromPlayer <= avoidPlayerDistance && distaneFromPlayer > .5f){ // if the mouse its too close, the slimes its going to go up insted going to a side;
                    StopCoroutine( Chill() );
                    Vector3 oppositeDirFromPlayer = -(PlayerMousePosition.mousePositionIn3dSpace - transform.position).normalized;
                    lastMoveDir = oppositeDirFromPlayer;
                    MoveTowards( oppositeDirFromPlayer, avoidSpeed, avoidJumpForce);

                }else if(isChilling == false){

                    if( IsCHillTime() ){
                        StartCoroutine( Chill() );
                    }else{
                        Vector3 moveDir = RandomDirection();
                        lastMoveDir = moveDir;
                        MoveTowards( moveDir, idleSpeed, idleJumpForce);
                    }
                }
                
            }
            else{

                MoveTowards( lastMoveDir, avoidSpeed, avoidJumpForce);
            }
        }
    }

    private void MoveTowards(Vector3 direction,float moveSpeed, float jumpForce){
            slimeRb.AddForce(Vector3.up * jumpForce);
            slimeRb.AddForce(direction * moveSpeed );
            isOnGround = false;
    }

    private Vector3 RandomDirection(){
        int xdir = Random.Range(-1,1);
        int zdir = Random.Range(-1,1);
        return new Vector3(xdir,0,zdir);
    }

    private bool IsCHillTime(){
        return Random.Range(0,100) <= chillProbability;
    }

    IEnumerator Chill(){
        isChilling = true;
        int chillTime = Random.Range(1,3);
        float i=0.0f;
        while( i< chillTime){
            yield return new WaitForSeconds(.5f);
            i += 0.5f;
        }
        isChilling = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground"))
            isOnGround = true;
    }
}
