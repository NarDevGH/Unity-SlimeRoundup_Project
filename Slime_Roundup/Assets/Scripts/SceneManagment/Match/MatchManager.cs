using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public enum MatchState {Playing,GameOver,Paused}
    public static MatchState s_CurrentMatchState { get; private set; }

    private static IMatchRoutine s_matchRoutine;

    private void Awake()
    {
        s_matchRoutine = GetComponent<IMatchRoutine>();
    }

    private void Start()
    {
        // startMatch on start method to let every component initialize before the match
        StartMatch();
    }

    private static void StartMatch()
    {
        s_CurrentMatchState = MatchState.Playing;
        s_matchRoutine.StartMatchRoutine();
    }

    public static void EndMatch()
    {
        s_CurrentMatchState = MatchState.GameOver;
        s_matchRoutine.EndMatchRoutine();
    }
}
