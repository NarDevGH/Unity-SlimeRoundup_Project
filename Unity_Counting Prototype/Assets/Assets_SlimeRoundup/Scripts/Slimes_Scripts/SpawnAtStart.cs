using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtStart : MonoBehaviour
{
    [SerializeField] private List<GameObject> objToSpawn;
    [SerializeField] private int ammountToSpawn = 1;
    [SerializeField] private Vector3 spawnCenter;
    [SerializeField] private Vector2 spawnDistanceRange;

    void Start()
    {
        SpawnObjs();
    }

    private void SpawnObjs(){
        for (int i = 0; i < ammountToSpawn; i++)
        {
            int objIndex = Random.Range(0,objToSpawn.Count);
            Instantiate(objToSpawn[objIndex],RandomPosition(spawnCenter), objToSpawn[objIndex].transform.rotation);
        }
    }

    Vector3 RandomPosition(Vector3 startpos){
        float xPos = Random.Range(-spawnDistanceRange.x,spawnDistanceRange.x)+startpos.x;
        float zPos = Random.Range(-spawnDistanceRange.y,spawnDistanceRange.y)+startpos.z;

        return new Vector3(xPos,startpos.y,zPos);
    }
}
