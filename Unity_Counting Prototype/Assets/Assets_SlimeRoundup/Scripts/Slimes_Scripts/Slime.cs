using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float avoidPlayerDistance = 2.0f;
    private Rigidbody slimeRb;

    private void Awake() {
        slimeRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distaneFromPlayer = (PlayerMousePosition.mousePositionIn3dSpace - transform.position).magnitude;
        if( distaneFromPlayer <= avoidPlayerDistance){
            Vector3 lookDirection = -(PlayerMousePosition.mousePositionIn3dSpace - transform.position).normalized;
            slimeRb.AddForce( lookDirection * speed,ForceMode.Force );
        }
    }
}
