using Slime_Roundup.Slime.DataTypes;
using System.Collections;
using UnityEngine;

// Dont Use as a component

public class Slime_TestBehavior : SlimeBehavior
{
    public Vector3 avoidDir { get; set; }

    [SerializeField] private str_MovementStats movement;

    private ISlimeMovement movementHandler;

    private void Awake()
    {
        movementHandler = GetComponent<ISlimeMovement>();
    }

    public override void StartBehavior()
    {
        if (avoidDir == Vector3.zero)
        {
            Debug.LogError("avoidDir hasnt been assigned yet!!! AvoidMouseRoutine didnt start");
            return;
        }

        if (behaviorRoutine != null) 
        {
            StopCoroutine(behaviorRoutine);
        }

        behaviorRoutine = StartCoroutine(BehaviorRoutine());
    }

    public override void EndBehavior()
    {
        StopCoroutine(behaviorRoutine);
        behaviorRoutine = null;
    }

    public override void ResumeBehavior()
    {
        if (behaviorPaused is false) return;

        behaviorPaused = false;
        return;
    }

    public override void PauseBehavior()
    {
        if (behaviorPaused) return;

        behaviorPaused = true;
        return;
    }

    public override IEnumerator BehaviorRoutine()
    {
        var jumpRoutine = StartCoroutine(movementHandler.JumpTowards(avoidDir, movement.speed, movement.jumpForce));
        yield return jumpRoutine;
    }
}
