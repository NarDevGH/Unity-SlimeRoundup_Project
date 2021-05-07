using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float idleSpeed = 5.0f;
    [SerializeField] private float avoidSpeed = 10.0f;
    [SerializeField] private float idleJumpForce = 10;
    [SerializeField] private float avoidJumpForce = 5.0f;
    [SerializeField] private float avoidPlayerDistance = 2.0f;


    private Rigidbody slimeRb;
    private bool isOnGround;
    private bool chilling;
    private float chillProbability;

    private void Awake() {
        slimeRb = GetComponent<Rigidbody>();
    }

    private void Start() {
        isOnGround = true;
        chilling = false;
        chillProbability = Random.Range(0,100);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround){
            float distaneFromPlayer = (PlayerMousePosition.mousePositionIn3dSpace - transform.position).magnitude;
            if( distaneFromPlayer <= avoidPlayerDistance){
                StopCoroutine( Chill() );
                Vector3 oppositeDirFromPlayer = -(PlayerMousePosition.mousePositionIn3dSpace - transform.position).normalized;
                Move( oppositeDirFromPlayer, avoidSpeed, avoidJumpForce);
            }else if(chilling == false){
                if( IsCHillTime() ){
                    StartCoroutine( Chill() );
                }else{
                    Move( RandomDirection(), idleSpeed, idleJumpForce);
                }
            }
        }
    }

    private void Move(Vector3 direction,float moveSpeed, float jumpForce){
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
        chilling = true;
        int chillTime = Random.Range(1,3);
        float i=0.0f;
        while( i< chillTime){
            yield return new WaitForSeconds(.5f);
            i += 0.5f;
        }
        chilling = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground"))
            isOnGround = true;
    }
}
