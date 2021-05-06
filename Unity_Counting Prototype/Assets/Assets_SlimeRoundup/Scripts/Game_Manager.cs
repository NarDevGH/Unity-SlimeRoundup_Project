using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Vector2 matchTime;
    [SerializeField] private Text timeText;
    private Vector2 currentMatchTime;

    private void Start() {
        currentMatchTime = matchTime;
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
                break;
            }
        }
    }
}
