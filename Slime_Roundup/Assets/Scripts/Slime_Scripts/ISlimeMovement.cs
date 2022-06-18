using System.Collections;
using UnityEngine;

public interface ISlimeMovement
{
    void StopMovementRoutines();

    IEnumerator JumpTowards(Vector3 direction, float jumpForce);
    IEnumerator JumpTowards(Vector3 direction, float speed, float jumpForce);
    IEnumerator GoTowardsRandomDirection(float speed, float jumpForce, int jumps = 1);
    IEnumerator GoTowards(Vector3 direction, float speed, float jumpForce, int jumps = 1);

}