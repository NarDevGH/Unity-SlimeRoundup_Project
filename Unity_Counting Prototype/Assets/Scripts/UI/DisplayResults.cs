using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private Text _capturedAmmountText;
    [SerializeField] private Text _scapedAmmountText;
    [SerializeField] private Text _timeElapsedText;
    [SerializeField] private SlimesManager _slimesManager;
    [SerializeField] private Game_Manager _gameManager;
    [SerializeField] private MatchTimer _matchTimer;
    

    private void Start() {
        _capturedAmmountText.text = _slimesManager.SlimesCaptured +"";
        _scapedAmmountText.text = (_slimesManager.CurrentInGameSlimes + _slimesManager.LosedSlimes)+"";
        _timeElapsedText.text ="Time Elapsed: "+ ( _gameManager.MatchTime.x - _matchTimer.CurrentMatchTime.x )+":"+(_gameManager.MatchTime.y - _matchTimer.CurrentMatchTime.y);
    }
}
