using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    [SerializeField] private int countdown;
    [SerializeField] private GameObject countdownText;

    [SerializeField] private SlimesManager _slimesManager;
    [SerializeField] private GameManager _gameManager;

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
