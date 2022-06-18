using System.Collections;
using UnityEngine;

public class Slime_BehaviorsHandler : MonoBehaviour
{
    public bool IsLost { get; private set; }
    public SlimeBehavior currentBehavior { get; private set; }

    public Slime_IdleBehavior idleBehavior { get; private set; }
    public Slime_ScapeBehavior scapeBehavior { get; private set; }
    public Slime_AvoidMouseBehavior avoidBehavior { get; private set; }
    public Slime_TakenByRiverBehavior takenByRiverBehavior { get; private set; }


    private Coroutine avoidRoutine;


    private void Awake()
    {
        idleBehavior = GetComponent<Slime_IdleBehavior>();
        scapeBehavior = GetComponent<Slime_ScapeBehavior>();
        avoidBehavior = GetComponent<Slime_AvoidMouseBehavior>();
        takenByRiverBehavior = GetComponent<Slime_TakenByRiverBehavior>();

        currentBehavior = idleBehavior;
        IsLost = false;
    }

    private void Start()
    {
        currentBehavior.StartBehavior();
    }

    public void ChangeTo_ScapeBehavior(Vector3 scapeDir) 
    {
        if (IsLost) return;

        IsLost = true;
        SlimesManager.Singleton.ScapedSlimes.Add(gameObject);

        StartCoroutine(ChangeRoutine_ScapeBehavior(scapeDir));
    }

    public void ChangeTo_TakenByRiverBehavior(Vector3 riverDir)
    {
        if (IsLost) return;

        IsLost = true;
        SlimesManager.Singleton.ScapedSlimes.Add(gameObject);

        StartCoroutine(ChangeRoutine_TakenByRiverBehavior(riverDir));
    }

    public void AvoidPoint(Vector3 point) 
    {
        if (IsLost) return;

        avoidRoutine = StartCoroutine(AvoidRoutine(point));
    }

    private IEnumerator AvoidRoutine(Vector3 point) 
    {
        currentBehavior.EndBehavior();

        Vector3 avoidDir = (transform.position - point).normalized;
        avoidBehavior.avoidDir = avoidDir;
        currentBehavior = avoidBehavior;
        currentBehavior.StartBehavior();

        yield return currentBehavior.behaviorRoutine;

        currentBehavior = idleBehavior;
        currentBehavior.StartBehavior();

        avoidRoutine = null;
    }

    private IEnumerator ChangeRoutine_ScapeBehavior(Vector3 scapeDir)
    {
        currentBehavior.EndBehavior();
        yield return null;
        scapeBehavior.scapeDir = scapeDir;
        currentBehavior = scapeBehavior;
        currentBehavior.StartBehavior();
    }
    private IEnumerator ChangeRoutine_TakenByRiverBehavior(Vector3 riverDir)
    {
        currentBehavior.EndBehavior();
        yield return null;
        takenByRiverBehavior.riverDir = riverDir;
        currentBehavior = takenByRiverBehavior;
        currentBehavior.StartBehavior();
    }

}
