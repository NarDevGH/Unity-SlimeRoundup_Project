using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    [SerializeField] private int countdown;
    [SerializeField] private GameObject countdownText;

    [SerializeField] private SlimesManager _slimesManager;
    [SerializeField] private Game_Manager _gameManager;

    private void Awake() {
        _slimesManager.allSlimesCaptured_Event += StartEndGameCountdown;
        _slimesManager.cancelSlimesCaptured_Event += CancelEndGameCountdown;
    }

    IEnumerator EndGameCountdown(){
        for (int i = countdown; i > 0; i--)
        {
            countdownText.SetActive(false);
            countdownText.SetActive(true);
            countdownText.GetComponent<Text>().text = i+"";
            yield return new WaitForSeconds(1);
        }
        _gameManager.GameOver();
    }

    private void StartEndGameCountdown(){
        StartCoroutine( "EndGameCountdown" );
    }
    private void CancelEndGameCountdown(){
        StopCoroutine( "EndGameCountdown" );
    }
}
