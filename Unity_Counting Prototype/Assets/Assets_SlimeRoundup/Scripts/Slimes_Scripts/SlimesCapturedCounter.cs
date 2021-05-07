using UnityEngine;
using UnityEngine.UI;

public class SlimesCapturedCounter : MonoBehaviour
{
    static public int slimesCamptured;
    [SerializeField] private Vector3 boxSize = new Vector3(1,1,1);
    [SerializeField] private LayerMask m_LayerMask;
    private bool m_Started;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale+boxSize, Quaternion.identity, m_LayerMask);
        
        slimesCamptured = hitColliders.Length;
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale+boxSize);
    }
}