using System.Collections.Generic;
using UnityEngine;

public class SlimesManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject[] _slimeVariants;
    [SerializeField] private Transform _catchArea;
    [SerializeField] private LayerMask _slimeLayer;


    private int _startingSlimesAmmount;
    private int _currentInGameSlimesAmmount;
    public int CurrentInGameSlimesAmmount{ get { return _currentInGameSlimesAmmount; } }

    private int _slimeCaptured;
    public int SlimesCaptured{ get{ return _slimeCaptured; } }

    private int _losedSlimes;
    public int LosedSlimes{ get{ return _losedSlimes; } }

    private List<GameObject> _currentSlimesInGame = new List<GameObject>();
    public List<GameObject> CurrentSlimesInGame { get { return _currentSlimesInGame; } }
    #endregion


    public event System.Action allSlimesCaptured_Event;
    public event System.Action cancelSlimesCaptured_Event;

    
    private bool allSlimesCaptured_WasTrigged = false;
    private bool allSlimesCaptured_Canceled = true;


    private void FixedUpdate() {
        DetectSlimesInsideCatchArea();
        _currentInGameSlimesAmmount = _startingSlimesAmmount - _slimeCaptured;

        if(_currentInGameSlimesAmmount == 0 && !allSlimesCaptured_WasTrigged){
            
            allSlimesCaptured_WasTrigged = true;
            allSlimesCaptured_Canceled = false;
            allSlimesCaptured_Event();
        }else if(!allSlimesCaptured_Canceled && _currentInGameSlimesAmmount != 0){
        
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
        _startingSlimesAmmount --;
    }

#region  SpawnSlimesFunctions
    public void SpawnSlime(Vector3 centerSpawnPoint){
        _startingSlimesAmmount ++;
        _currentInGameSlimesAmmount ++;
        int randSlimeIndex = Random.Range(0,_slimeVariants.Length);

        GameObject newSlime = Instantiate(_slimeVariants[randSlimeIndex],
                                          new Vector3(centerSpawnPoint.x,centerSpawnPoint.y,centerSpawnPoint.z), 
                                          _slimeVariants[randSlimeIndex].transform.rotation);
        _currentSlimesInGame.Add(newSlime);
    }

    public void SpawnSlime(Vector3 centerSpawnPoint,Vector2 spawnRange){
        _startingSlimesAmmount ++;
        _currentInGameSlimesAmmount ++;

        int randSlimeIndex = Random.Range(0,_slimeVariants.Length);
        float xPos = Random.Range(-spawnRange.x,spawnRange.x)+centerSpawnPoint.x;
        float zPos = Random.Range(-spawnRange.y,spawnRange.y)+centerSpawnPoint.z;

        GameObject newSlime = Instantiate(_slimeVariants[randSlimeIndex],
                                          new Vector3(xPos,centerSpawnPoint.y,zPos), 
                                          _slimeVariants[randSlimeIndex].transform.rotation);
        _currentSlimesInGame.Add(newSlime);
    }

    public void SpawnSlimes(int ammountToSpawn, Vector3 centerSpawnPoint){
        _startingSlimesAmmount += ammountToSpawn;
        _currentInGameSlimesAmmount += ammountToSpawn;

        for (int i = 0; i < ammountToSpawn; i++)
        {
            int randSlimeIndex = Random.Range(0,_slimeVariants.Length);

            GameObject newSlime;
            newSlime = Instantiate(_slimeVariants[randSlimeIndex],
                                              new Vector3(centerSpawnPoint.x,centerSpawnPoint.y,centerSpawnPoint.z), 
                                              _slimeVariants[randSlimeIndex].transform.rotation);
            _currentSlimesInGame.Add(newSlime);
        }
    }

    public void SpawnSlimes(int ammountToSpawn, Vector3 centerSpawnPoint,Vector2 spawnRange){
        _startingSlimesAmmount += ammountToSpawn;
        _currentInGameSlimesAmmount += ammountToSpawn;
        for (int i = 0; i < ammountToSpawn; i++)
        {
            int randSlimeIndex = Random.Range(0,_slimeVariants.Length);
            float xPos = Random.Range(-spawnRange.x,spawnRange.x)+centerSpawnPoint.x;
            float zPos = Random.Range(-spawnRange.y,spawnRange.y)+centerSpawnPoint.z;

            GameObject newSlime;
            newSlime = Instantiate(_slimeVariants[randSlimeIndex],
                                              new Vector3(xPos,centerSpawnPoint.y,zPos), 
                                              _slimeVariants[randSlimeIndex].transform.rotation);
            _currentSlimesInGame.Add(newSlime);
        }
    }

#endregion

}
