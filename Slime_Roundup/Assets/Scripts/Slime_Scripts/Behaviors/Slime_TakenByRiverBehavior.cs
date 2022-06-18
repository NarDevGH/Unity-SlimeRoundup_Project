using System.Collections;
using UnityEngine;

public class Slime_TakenByRiverBehavior : SlimeBehavior
{
    public Vector3 riverDir { get; set; }

    [SerializeField, Range(0, 360)] private float spinSpeed;
    [SerializeField] private float upAndDownSpeed;
    [SerializeField] private float riverSpeed;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        riverDir = Vector3.zero;
    }

    public override void StartBehavior()
    {
        if (riverDir == Vector3.zero)
        {
            Debug.LogError("riverDir hasnt been assigned yet!!! TakenByRiverRoutine didnt start");
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

        riverDir = Vector3.zero;
        StopCoroutine(behaviorRoutine);
        behaviorRoutine = null;
        return;
    }

    public override void PauseBehavior()
    {
        if (behaviorPaused) return;

        behaviorPaused = true;
        return;
    }

    public override void ResumeBehavior()
    {
        if (behaviorPaused is false) return;

        behaviorPaused = false;
        return;
    }

    public override IEnumerator BehaviorRoutine()
    {
        rb.isKinematic = true;
        while (true)
        {
            while (behaviorPaused)
            {
                yield return null;
            }

            // make slime rotate
            transform.Rotate(Vector3.up, spinSpeed);

            // make slime move following the river
            transform.Translate(riverDir * riverSpeed, Space.World);

            // make the slime up and down as is floating

            yield return null;
        }
    }


}