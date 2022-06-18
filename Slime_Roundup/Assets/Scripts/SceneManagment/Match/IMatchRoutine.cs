using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatchRoutine
{
    public void StartMatchRoutine();
    public void PauseMatchRoutine();
    public void ResumeMatchRoutine();
    public void EndMatchRoutine();
}
