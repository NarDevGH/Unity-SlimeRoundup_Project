using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMousePosition : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    static public Vector3 mousePositionIn3dSpace;
    private void Update() {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast (ray, out hit) ){
            mousePositionIn3dSpace = hit.point;
        }
    }
}
