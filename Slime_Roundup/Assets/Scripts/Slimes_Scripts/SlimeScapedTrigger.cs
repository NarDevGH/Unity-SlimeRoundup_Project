using UnityEngine;

public class SlimeScapedTrigger : MonoBehaviour
{
    [SerializeField] private SlimesManager _slimesManager;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Slime")){
            if( other.GetComponent<Slime_LostBehaviour>().IsLost == false){
                other.GetComponent<Slime_LostBehaviour>().SetSlimeAsLost();
                _slimesManager.SlimeScaped();
            }
        }
    }
}
