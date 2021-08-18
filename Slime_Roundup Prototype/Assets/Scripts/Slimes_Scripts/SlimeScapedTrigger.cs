using UnityEngine;

public class SlimeScapedTrigger : MonoBehaviour
{
    [SerializeField] private SlimesManager _slimesManager;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Slime")){
            if( other.GetComponent<Slime>().IsLost == false){
                other.GetComponent<Slime>().IsLost = true;
                _slimesManager.SlimeScaped();
            }
        }
    }
}
