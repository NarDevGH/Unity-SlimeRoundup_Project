using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private Text capturedAmmountText;
    [SerializeField] private Text scapedAmmountText;
    

    private void Start() {
        capturedAmmountText.text = SlimesCapturedCounter.slimesCamptured+"";
        scapedAmmountText.text = (SpawnSlimesAtStart.slimesToSpawn - SlimesCapturedCounter.slimesCamptured)+"";
    }
}
