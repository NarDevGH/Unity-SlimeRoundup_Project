using System.Collections;
using UnityEngine;
using Slime_Roundup.Slime.DataTypes;

public class Slime_ScapeBehavior : SlimeBehavior
{
    public Vector3 scapeDir { get; set; }

    [SerializeField] private str_MovementStats movement;

    private ISlimeMovement movementHandler;


    private void Awake()
    {
        movementHandler = GetComponent<ISlimeMovement>();
        scapeDir = Vector3.zero;
    }

    public override void StartBehavior()
    {
        if (scapeDir == Vector3.zero)
        {
            Debug.LogError("scapeDir hasnt been assigned yet!!! IdleBehaviorRoutine didnt start");
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

        scapeDir = Vector3.zero;
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
        while (true)
        {
            while (behaviorPaused)
            {
                yield return null;
            }

            var jumpRoutine = StartCoroutine(movementHandler.GoTowards(scapeDir, movement.speed, movement.jumpForce));
            yield return jumpRoutine;

            yield return null;
        }
    }
}