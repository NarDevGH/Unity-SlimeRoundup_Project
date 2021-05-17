using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] private Text _currentMatchTimeText;
    [SerializeField] private Game_Manager _gameManager;
    private Vector2 _currentMatchTime;

    void Start()
    {
        _currentMatchTime = _gameManager.MatchTime;
        UpdateMatchTimeText();
        StartCoroutine( Timer() );
    }

    private void UpdateMatchTimeText(){
        _currentMatchTimeText.text = _currentMatchTime.x+":"+_currentMatchTime.y;
    }

    private void OnTimerReachCero(){
        _gameManager.GameOver();
    }

    IEnumerator Timer(){
        while(true){
            yield return new WaitForSeconds(1);
            if(_currentMatchTime.y > 0){
                _currentMatchTime.y--;
            }else if(_currentMatchTime.x > 0){
                _currentMatchTime.x--;
                _currentMatchTime.y = 59;
            }else{
                OnTimerReachCero();
            }
            UpdateMatchTimeText();
        }
    }

}
