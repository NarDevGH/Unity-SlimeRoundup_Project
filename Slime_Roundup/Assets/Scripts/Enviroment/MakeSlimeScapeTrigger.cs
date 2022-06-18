using System;
using UnityEngine;

public class MakeSlimeScapeTrigger : MonoBehaviour
{
    [SerializeField] private ScapeDir scapeDir;

    [Serializable]
    private enum ScapeDir {North,South,West,East }

    private Vector3 ScapeDirToVec3(ScapeDir scapeDir) 
    {
        return scapeDir switch
        {
            ScapeDir.North => Vector3.forward,
            ScapeDir.South => Vector3.back,
            ScapeDir.West => Vector3.left,
            ScapeDir.East => Vector3.right,
            _ => throw new Exception("Bad info passed in")
        };
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Slime") == false) return;

        Slime_BehaviorsHandler slime = other.GetComponent<Slime_BehaviorsHandler>();
        if (slime == null) 
        {
            Debug.LogError("Slime_BehaviorsHandler Missing !! slime never ChangeToLostBehavior");
            return;
        }

        if (slime.IsLost) return;

        slime.ChangeTo_ScapeBehavior(ScapeDirToVec3(scapeDir));
    }
}
