using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTakenRiverTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 riverDir;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime") is false) return;

        Slime_BehaviorsHandler slime = other.GetComponent<Slime_BehaviorsHandler>();
        if (slime == null)
        {
            Debug.LogError("Slime_BehaviorsHandler Missing !! slime never ChangeToTakenByRiverBehavior");
            return;
        }

        if (slime.IsLost) return;

        slime.ChangeTo_TakenByRiverBehavior(riverDir);
    }
}
