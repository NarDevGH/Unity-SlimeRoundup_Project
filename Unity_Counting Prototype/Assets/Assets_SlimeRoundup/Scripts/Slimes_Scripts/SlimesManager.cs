using UnityEngine;

public class SlimesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _slimes;
    [SerializeField] private Transform _catchArea;
    [SerializeField] private LayerMask _slimeLayer;
    
    private int _inGameSlimes;
    private int _currentInGameSlimes;
    public int CurrentInGameSlimes{
        get { return _currentInGameSlimes; }
    }

    private int _slimeCaptured;
    public int SlimesCaptured{
        get{ return _slimeCaptured; }
    }

    private int _losedSlimes;
    public int LosedSlimes{
        get{ return _losedSlimes; }
    }
    
    public event System.Action allSlimesCaptured_Event;
    public event System.Action cancelSlimesCaptured_Event;

    
    private bool allSlimesCaptured_WasTrigged = false;
    private bool allSlimesCaptured_Canceled = true;
    private void FixedUpdate() {
        DetectSlimesInsideCatchArea();
        _currentInGameSlimes = _inGameSlimes - _slimeCaptured;

        if(_currentInGameSlimes == 0 && !allSlimesCaptured_WasTrigged){
            
            allSlimesCaptured_WasTrigged = true;
            allSlimesCaptured_Canceled = false;
            allSlimesCaptured_Event();
        }else if(!allSlimesCaptured_Canceled && _currentInGameSlimes != 0){
        
            allSlimesCaptured_Canceled = true;
            allSlimesCaptured_WasTrigged = false;
            cancelSlimesCaptured_Event();
        }
    }


    private void DetectSlimesInsideCatchArea(){
        Collider[] slimesInside = Physics.OverlapBox(_catchArea.position, _catchArea.localScale, Quaternion.identity, _slimeLayer);
        
        _slimeCaptured = slimesInside.Length;
    }

    public void SlimeScaped(){
        _losedSlimes ++;
        _inGameSlimes --;
    }

#region  SpawnSlimesFunctions
    public void SpawnSlime(Vector3 centerSpawnPoint){
        _inGameSlimes ++;
        _currentInGameSlimes ++;
        int randSlimeIndex = Random.Range(0,_slimes.Length);

        Instantiate(_slimes[randSlimeIndex],
                    new Vector3(centerSpawnPoint.x,centerSpawnPoint.y,centerSpawnPoint.z), 
                    _slimes[randSlimeIndex].transform.rotation);
    }

    public void SpawnSlime(Vector3 centerSpawnPoint,Vector2 spawnRange){
        _inGameSlimes ++;
        _currentInGameSlimes ++;

        int randSlimeIndex = Random.Range(0,_slimes.Length);
        float xPos = Random.Range(-spawnRange.x,spawnRange.x)+centerSpawnPoint.x;
        float zPos = Random.Range(-spawnRange.y,spawnRange.y)+centerSpawnPoint.z;
        Instantiate(_slimes[randSlimeIndex],
                    new Vector3(xPos,centerSpawnPoint.y,zPos), 
                    _slimes[randSlimeIndex].transform.rotation);
    }

    public void SpawnSlimes(int ammountToSpawn, Vector3 centerSpawnPoint){
        _inGameSlimes += ammountToSpawn;
        _currentInGameSlimes += ammountToSpawn;

        for (int i = 0; i < ammountToSpawn; i++)
        {
            int randSlimeIndex = Random.Range(0,_slimes.Length);

            Instantiate(_slimes[randSlimeIndex],
                        new Vector3(centerSpawnPoint.x,centerSpawnPoint.y,centerSpawnPoint.z), 
                        _slimes[randSlimeIndex].transform.rotation);
        }
    }

    public void SpawnSlimes(int ammountToSpawn, Vector3 centerSpawnPoint,Vector2 spawnRange){
        _inGameSlimes += ammountToSpawn;
        _currentInGameSlimes += ammountToSpawn;
        for (int i = 0; i < ammountToSpawn; i++)
        {
            int randSlimeIndex = Random.Range(0,_slimes.Length);
            float xPos = Random.Range(-spawnRange.x,spawnRange.x)+centerSpawnPoint.x;
            float zPos = Random.Range(-spawnRange.y,spawnRange.y)+centerSpawnPoint.z;
            Instantiate(_slimes[randSlimeIndex],
                        new Vector3(xPos,centerSpawnPoint.y,zPos), 
                        _slimes[randSlimeIndex].transform.rotation);
        }
    }

#endregion

}
