using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeBehavior : MonoBehaviour
{
    public bool behaviorPaused { get; protected set; }
    public Coroutine behaviorRoutine { get; protected set; }

    public abstract void StartBehavior();
    public abstract void EndBehavior();
    public abstract void PauseBehavior();
    public abstract void ResumeBehavior();
    public abstract IEnumerator BehaviorRoutine();
}
