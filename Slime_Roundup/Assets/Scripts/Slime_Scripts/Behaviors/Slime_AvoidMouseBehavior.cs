using UnityEngine;
using Slime_Roundup.Slime.DataTypes;
using System.Collections;

public class Slime_AvoidMouseBehavior : SlimeBehavior
{
    public Vector3 avoidDir { get; set; }

    [SerializeField] private str_MovementStats movement;

    private ISlimeMovement movementHandler;


    private void Awake()
    {
        movementHandler = GetComponent<ISlimeMovement>();
        avoidDir = Vector3.zero;
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
        return;
    }

    public override void EndBehavior()
    {
        if (behaviorRoutine is null) return;

        avoidDir = Vector3.zero;
        StopCoroutine(behaviorRoutine);
        behaviorRoutine = null;
        return;
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
