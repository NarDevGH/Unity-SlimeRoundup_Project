using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimer : MonoBehaviour
{
    public int Time { get; private set; }

    [SerializeField,Min(1)] private int countdown = 3;

    [SerializeField] private Text textLabel;
    [SerializeField] private EndGameCountdown endGameCountdown;

    private Coroutine timerRoutine;

    private void Awake()
    {
        timerRoutine = null;
    }

    public void StartTimerRoutine(int matchDuration) 
    {
        if (timerRoutine != null) StopCoroutine(timerRoutine);
        timerRoutine = StartCoroutine(TimerRoutine(matchDuration));
    }
    public void StopTimerRoutine() 
    {
        if (timerRoutine is null) return;

        StopCoroutine(timerRoutine);
        timerRoutine=null;
    }

    public void AddTime(int amount) 
    {
        if (Time > 0)
        {
            Time += amount;
        }
        else 
        {
            Debug.LogError($"Timer reach 0, cant add {amount} sec. to the timer");
        }

        return;
    }
    public void RemoveTime(int amount) 
    {
        if (Time <= 0) 
        {
            Debug.LogError($"Timer already at 0, cant remove {amount} sec. !!!");
            return;
        }

        Time -= ((Time - amount) >= 0) ? Time - amount : Time;
    }


    private void SetTimerText() 
    {
        int minutes = Mathf.RoundToInt(Time / 60);
        int seconds = Time % 60;
        textLabel.text = $"{minutes}:{seconds}";
    }
    private IEnumerator TimerRoutine(int matchDuration){
        Time = matchDuration;
        SetTimerText();
        while (Time > countdown)
        {
            yield return new WaitForSeconds(1);
            Time--;
            SetTimerText();
        }
        textLabel.gameObject.SetActive(false);
        endGameCountdown.StartCountdown(countdown);
    }

}
