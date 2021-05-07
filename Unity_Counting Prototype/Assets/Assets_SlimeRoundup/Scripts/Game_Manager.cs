using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Vector2 matchTime;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject gameOverPanel;
    private Vector2 currentMatchTime;

    private void Start() {
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
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                break;
            }
        }
    }
}
