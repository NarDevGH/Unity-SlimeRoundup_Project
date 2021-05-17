using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SlimesManager))]
public class Game_Manager : MonoBehaviour
{
    [SerializeField] private SlimesManager _slimesManager;
    [SerializeField] private Canvas uICanvas;
    [SerializeField] private Vector2 matchTime;
    [SerializeField] private Text timeText;
    [SerializeField] private int countdown;
    [SerializeField] private GameObject countdownText;
    [SerializeField] private GameObject gameOverPanel;

    private Vector2 currentMatchTime;

    private void Start() {
        _slimesManager.allSlimesCaptured_Event += StartEndGameCountdown;
        _slimesManager.cancelSlimesCaptured_Event += CancelEndGameCountdown;
        currentMatchTime = matchTime;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine( Timer() );
    }


    IEnumerator Timer(){
        while(true){
            timeText.text = currentMatchTime.x+":"+currentMatchTime.y;
            yield return new WaitForSeconds(1);
            if(currentMatchTime.y > 0){
                currentMatchTime.y--;
            }else if(currentMatchTime.x > 0){
                currentMatchTime.x--;
                currentMatchTime.y = 59;
            }else{
                GameOver();
                break;
            }
        }
    }

    public void GameOver(){
        StopAllCoroutines();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    IEnumerator EndGameTimer(){
        int i = countdown;
        while(true){
            if(i > 0){
                countdownText.SetActive(false);
                countdownText.SetActive(true);
                countdownText.GetComponent<Text>().text = i+"";
                i--;
                yield return new WaitForSeconds(1);
            }else{
                GameOver();
                break;
            }
        }
    }

    private void StartEndGameCountdown(){
        StartCoroutine( "EndGameTimer" );
    }
    private void CancelEndGameCountdown(){
        StopCoroutine( "EndGameTimer" );
    }
}
