using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadSlimesWithMouse : MonoBehaviour
{
    [SerializeField] float interactionRange;
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask playableAreaLayer;
    [SerializeField] LayerMask slimeLayer;

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, playableAreaLayer))
        {
            Collider[] slimesInRange = Physics.OverlapSphere(hit.point, interactionRange, slimeLayer);
            foreach (Collider slime in slimesInRange) 
            {
                if (slime.GetComponent<Slime>().IsGrounded) 
                {
                    Vector3 oppositeDirFromPlayer = -(hit.point - slime.transform.position).normalized;
                    slime.GetComponent<Slime>().AvoidTowardsDir(oppositeDirFromPlayer);                
                }
            }
        }
        

    }
}
