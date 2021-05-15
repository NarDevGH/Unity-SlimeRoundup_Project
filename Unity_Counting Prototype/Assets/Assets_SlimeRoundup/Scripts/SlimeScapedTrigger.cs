using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScapedTrigger : MonoBehaviour
{
    [SerializeField] private SlimesManager _slimesManager;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Slime")){
            if( other.GetComponent<Slime>().isLost == false){
                other.GetComponent<Slime>().isLost = true;
                _slimesManager.SlimeScaped();
            }
        }
    }
}
