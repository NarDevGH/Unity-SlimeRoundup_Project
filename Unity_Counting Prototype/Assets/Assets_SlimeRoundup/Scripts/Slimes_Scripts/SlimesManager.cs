using UnityEngine;
using UnityEngine.Events;

public class SlimesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _slimes;
    public static GameObject[] slimes;
    
    private int _slimeCaptured;
    public int SlimesCaptured{
        get{ return _slimeCaptured; }
    }
    private int _losedSlimes;
    public int LosedSlimes{
        get{ return _losedSlimes; }
        set{ _losedSlimes = value; }
    }
    private int _inGameSlimes;
    [SerializeField] private Transform _catchArea;
    [SerializeField] private LayerMask _slimeLayer;

    public UnityEvent m_allCapturedEvent;

    private void Awake() {
        slimes = _slimes;
        
    }

    private void Start() {

        if(m_allCapturedEvent == null){
            m_allCapturedEvent = new UnityEvent(); 
        }
    }
    
    private void FixedUpdate() {
        DetectSlimesInsideCatchArea();
        if(_inGameSlimes == _slimeCaptured){
            m_allCapturedEvent.Invoke();
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

    public void SpawnSlime(Vector3 centerSpawnPoint){
        _inGameSlimes ++;
        int randSlimeIndex = Random.Range(0,SlimesManager.slimes.Length);

        Instantiate(SlimesManager.slimes[randSlimeIndex],
                    new Vector3(centerSpawnPoint.x,centerSpawnPoint.y,centerSpawnPoint.z), 
                    SlimesManager.slimes[randSlimeIndex].transform.rotation);
    }

    public void SpawnSlime(Vector3 centerSpawnPoint,Vector2 spawnRange){
        _inGameSlimes ++;

        int randSlimeIndex = Random.Range(0,SlimesManager.slimes.Length);
        float xPos = Random.Range(-spawnRange.x,spawnRange.x)+centerSpawnPoint.x;
        float zPos = Random.Range(-spawnRange.y,spawnRange.y)+centerSpawnPoint.z;
        Instantiate(SlimesManager.slimes[randSlimeIndex],
                    new Vector3(xPos,centerSpawnPoint.y,zPos), 
                    SlimesManager.slimes[randSlimeIndex].transform.rotation);
    }

    public void SpawnSlimes(int ammountToSpawn, Vector3 centerSpawnPoint){
        _inGameSlimes += ammountToSpawn;

        for (int i = 0; i < ammountToSpawn; i++)
        {
            int randSlimeIndex = Random.Range(0,SlimesManager.slimes.Length);

            Instantiate(SlimesManager.slimes[randSlimeIndex],
                        new Vector3(centerSpawnPoint.x,centerSpawnPoint.y,centerSpawnPoint.z), 
                        SlimesManager.slimes[randSlimeIndex].transform.rotation);
        }
    }

    public void SpawnSlimes(int ammountToSpawn, Vector3 centerSpawnPoint,Vector2 spawnRange){
        _inGameSlimes += ammountToSpawn;
        for (int i = 0; i < ammountToSpawn; i++)
        {
            int randSlimeIndex = Random.Range(0,SlimesManager.slimes.Length);
            float xPos = Random.Range(-spawnRange.x,spawnRange.x)+centerSpawnPoint.x;
            float zPos = Random.Range(-spawnRange.y,spawnRange.y)+centerSpawnPoint.z;
            Instantiate(SlimesManager.slimes[randSlimeIndex],
                        new Vector3(xPos,centerSpawnPoint.y,zPos), 
                        SlimesManager.slimes[randSlimeIndex].transform.rotation);
        }
    }
    

}
