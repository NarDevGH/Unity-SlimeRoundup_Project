using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private SlimesManager _slimesManager;
    [SerializeField] private Text capturedAmmountText;
    [SerializeField] private Text scapedAmmountText;
    

    private void Start() {
        capturedAmmountText.text = _slimesManager.SlimesCaptured +"";
        scapedAmmountText.text = (SpawnSlimesAtStart.slimesToSpawn - _slimesManager.SlimesCaptured)+"";
    }
}
