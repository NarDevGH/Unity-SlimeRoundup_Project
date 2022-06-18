using UnityEngine;

public class LeadSlimesWithMouse : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask playableAreaLayer;
    [SerializeField] private LayerMask slimeLayer;


    // Gets all slimes inside mouse interacionRange and tell them to avoid mouse positon.
    private void Update()
    {
        //if not playing return.
        if (MatchManager.s_CurrentMatchState != MatchManager.MatchState.Playing) return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, playableAreaLayer))
        {
            Collider[] slimesInRange = Physics.OverlapSphere(hit.point, interactionRange, slimeLayer);

            foreach (Collider slime in slimesInRange) 
            {
                slime.GetComponent<Slime_BehaviorsHandler>().AvoidPoint(hit.point);
            }
        }
        

    }
}
