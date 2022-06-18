using System.Collections;
using UnityEngine;


public class SimpleMatchRoutine : MonoBehaviour,IMatchRoutine
{
    public int MatchDuration { get { return matchDuration; } }
    public MatchTimer Timer { get { return timer; } }

    [SerializeField, Min(1)] private int matchDuration = 120;
    [SerializeField] private int ammountToSpawn = 10;
    [SerializeField, Min(1)] private int countdown = 3;
    [SerializeField] private MatchTimer timer;
    [SerializeField] private Slime_SpawnManager spawnManager;
    [SerializeField] private EndGameCountdown endGameCountdown;
    [SerializeField] private GameObject gameOverPanel;

    // save the references in static variables, to keep them when the scene gets reloaded.
    private static MatchTimer s_Timer;
    private static Slime_SpawnManager s_SpawnManager;
    private static GameObject s_GameoverPanel;

    private void Awake()
    {
        s_Timer = timer;
        s_SpawnManager = spawnManager;
        s_GameoverPanel = gameOverPanel;
    }


    private IEnumerator CheckIfEndGameSoonerRoutine()
    {
        while (MatchManager.s_CurrentMatchState == MatchManager.MatchState.Playing) 
        {
            if (SlimesManager.Singleton.InGameSlimes_Ammount == 0) 
            {
                if (SlimesManager.Singleton.CapturedSlimes_Ammount <= 0)
                {
                    StartCoroutine(EndGameSoonerRoutine());
                    break;
                }
                else
                {
                    //EndGameSoonerWithCountdownRoutine
                    timer.StartTimerRoutine(countdown);
                    break;
                }
            }
            yield return null;
        }
    }

    private IEnumerator EndGameSoonerRoutine() 
    {
        yield return new WaitForSeconds(2);
        EndMatchRoutine();
    }


    public void StartMatchRoutine()
    {
        s_Timer.StartTimerRoutine(matchDuration);

        var slimes = s_SpawnManager.SpawnSlimes(ammountToSpawn);
        SlimesManager.Singleton.AllSlimes = slimes;

        StartCoroutine(CheckIfEndGameSoonerRoutine());
    }

    public void EndMatchRoutine()
    {
        StopAllCoroutines();
        SlimesManager.Singleton.StopAllCapturedSlimes();
        SlimesManager.Singleton.SetInGameSlimesAsScaped();
        s_GameoverPanel.SetActive(true);
    }

    public void PauseMatchRoutine()
    {
        throw new System.NotImplementedException();
    }

    public void ResumeMatchRoutine()
    {
        throw new System.NotImplementedException();
    }

}
