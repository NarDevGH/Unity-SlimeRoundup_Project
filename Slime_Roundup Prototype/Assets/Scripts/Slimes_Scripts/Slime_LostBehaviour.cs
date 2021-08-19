using UnityEngine;

public class Slime_LostBehaviour : MonoBehaviour
{
    private Slime_LogicHandler _slimeLogicHandler;
    private Slime_PhysicsBasedMovement _slimeMovementHandler; 
    private bool _isLost = false;
    public bool IsLost { get { return _isLost; } }

    private void Awake()
    {
        _slimeLogicHandler = GetComponent<Slime_LogicHandler>();
        _slimeMovementHandler = _slimeLogicHandler.MovementHandler;
    }

    private void Update()
    {
        if (_isLost) 
        {
            _slimeMovementHandler.MoveTowardsDir(_slimeMovementHandler.LastMoveDirection);
        }
    }

    public void SetSlimeAsLost()
    {
        _isLost = true;
        _slimeLogicHandler.enabled = false;
    }
}
