using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField,Min(0)] private float speed;
    [SerializeField,Min(0)] private float jumpForce;
    [SerializeField] private Slime_PhysicsBasedMovement movementHandler;

    private void Awake()
    {
        movementHandler ??= GetComponent<Slime_PhysicsBasedMovement>();
    }

    private void OnEnable()
    {
        StartCoroutine(TestMovementRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator TestMovementRoutine()
    {
        while (true) 
        {
            //transform.LookAt(Vector3.left);
            yield return StartCoroutine(movementHandler.GoTowards(Vector3.left, speed, jumpForce));
            yield return null;
        }
    }
}
