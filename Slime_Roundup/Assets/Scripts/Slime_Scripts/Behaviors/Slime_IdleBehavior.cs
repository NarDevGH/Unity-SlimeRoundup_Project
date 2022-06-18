using System.Collections;
using UnityEngine;
using Slime_Roundup.Slime.DataTypes;

public class Slime_IdleBehavior : SlimeBehavior
{
    [SerializeField] private str_MovementStats movement;
    [SerializeField] private str_jumps consecutiveJumps;
    [SerializeField] private str_RestStats rest;

    private Slime_PhysicsBasedMovement movementHandler;


    private void Awake()
    {
        movementHandler = GetComponent<Slime_PhysicsBasedMovement>();
    }

    public override void StartBehavior()
    {
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
        yield return new WaitUntil(() => movementHandler.IsGrounded);

        while (true)
        {
            while (behaviorPaused)
            {
                yield return null;
            }

            bool ShouldRest = Random.Range(0, 100) <= rest.probability;
            if (ShouldRest)
            {
                yield return StartCoroutine(RestRoutine());
            }
            else
            {
                int jumps = Random.Range(consecutiveJumps.min, consecutiveJumps.max);
                yield return StartCoroutine(movementHandler.GoTowardsRandomDirection(movement.speed, movement.jumpForce, jumps));
            }
        }
    }

    private IEnumerator RestRoutine()
    {
        float restTime = Random.Range(rest.minTime, rest.maxTime);
        yield return new WaitForSeconds(restTime);
    }

    
}