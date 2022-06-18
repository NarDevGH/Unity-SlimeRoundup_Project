using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCountdown : MonoBehaviour
{
    [SerializeField] private Text textLabel;
    [SerializeField] private Animator animator;

    private bool textAppearEnded 
    {
        get 
        {
            if (_textAppearEnded)
            {
                _textAppearEnded = false;
                return true;
            }
            else 
            {
                return false;
            }
        }
        set { _textAppearEnded = value; }
    }
    private bool _textAppearEnded;

    private Coroutine countdownRoutine;

    private void Awake()
    {
        textAppearEnded = false;
    }

    public void StartCountdown(int value) 
    {
        if (value < 1) 
        {
            Debug.LogWarning($"Countdown never start, bc low value({value})");
            MatchManager.EndMatch();
            return;
        }

        if(countdownRoutine != null) StopCoroutine(countdownRoutine);

        countdownRoutine = StartCoroutine(CountdownRoutine(value));
    }

    public void StopCountdown()
    {
        StopCoroutine(countdownRoutine);
        countdownRoutine = null;
    }

    private IEnumerator CountdownRoutine(int value) 
    {
        for (int i = value; i > 0; i--)
        {
            textLabel.text = $"{i}";
            animator.Play("TextAppear");
            yield return new WaitUntil(() => textAppearEnded);
        }
        textLabel.text = $"";
        MatchManager.EndMatch();
    }

    public void OnTextAppearEnded() 
    {
        textAppearEnded = true;
    }
}
