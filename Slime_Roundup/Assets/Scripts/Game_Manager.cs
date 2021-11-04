using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Vector2 _matchTime;
    public Vector2 MatchTime{
        get {return _matchTime; }
    }
    [SerializeField] private GameObject gameOverPanel;


    private void Start() {
        Time.timeScale = 1;
    }


    public void GameOver(){
        StopAllCoroutines();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    
}
