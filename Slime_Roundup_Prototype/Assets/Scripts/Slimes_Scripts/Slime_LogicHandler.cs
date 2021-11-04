using UnityEngine;

public class Slime_LogicHandler : MonoBehaviour
{

    [SerializeField] private float _minRestTime;
    [SerializeField] private float _maxRestTime;


    private bool _resting;
    private double _RestingProbability;
    private float _restingTimer;

    private Slime_PhysicsBasedMovement _movementHandler;
    public Slime_PhysicsBasedMovement MovementHandler { get { return _movementHandler; } }

    #region INIT
    private void Awake()
    {
        _movementHandler = GetComponent<Slime_PhysicsBasedMovement>();
    }

    private void Start()
    {
        _resting = false;
        _RestingProbability = Random.Range(0, 100);
    }
    #endregion

    void Update()
    {
        if (_resting == false)
        {

            if (WantToRest())
            {
                StartResting();
            }
            else
            {

                _movementHandler.MoveTowardRandomDirection();
            }
        }
        else
        {
            Rest();
        }
    }

    private void StartResting()
    {
        _resting = true;
        _restingTimer = Random.Range(_minRestTime, _maxRestTime);
    }

    private void Rest()
    {
        if (_restingTimer <= 0f )
        {
            _resting = false;
        }
        else
        {
            _restingTimer -= Time.deltaTime;
        }
    }

    private bool WantToRest()
    {
        return Random.Range(0, 100) <= _RestingProbability;
    }

}
