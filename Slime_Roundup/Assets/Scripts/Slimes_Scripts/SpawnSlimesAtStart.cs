using System.Collections.Generic;
using UnityEngine;

public class SpawnSlimesAtStart : MonoBehaviour
{
    static public int slimesToSpawn = 8;
    [SerializeField] private List<GameObject> slimes;
    [SerializeField] private Vector3 spawnCenter;
    [SerializeField] private Vector2 spawnDistanceRange;

    void Start()
    {
        SpawnObjs();
    }

    private void SpawnObjs(){
        for (int i = 0; i < slimesToSpawn; i++)
        {
            int objIndex = Random.Range(0,slimes.Count);
            Instantiate(slimes[objIndex],RandomPosition(spawnCenter), slimes[objIndex].transform.rotation);
        }
    }

    Vector3 RandomPosition(Vector3 startpos){
        float xPos = Random.Range(-spawnDistanceRange.x,spawnDistanceRange.x)+startpos.x;
        float zPos = Random.Range(-spawnDistanceRange.y,spawnDistanceRange.y)+startpos.z;

        return new Vector3(xPos,startpos.y,zPos);
    }
}
