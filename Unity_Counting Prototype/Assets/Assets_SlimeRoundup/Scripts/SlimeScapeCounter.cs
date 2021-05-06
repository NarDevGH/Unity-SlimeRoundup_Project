using UnityEngine;
using UnityEngine.UI;

public class SlimeScapeCounter : MonoBehaviour
{
    public Text counterText;
    static public int slimeScapeCounter = 0;
    
    private void FixedUpdate() {
        counterText.text = ""+slimeScapeCounter;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Slime")){
            slimeScapeCounter++;
            Destroy(other.gameObject);
        }
    }
}
