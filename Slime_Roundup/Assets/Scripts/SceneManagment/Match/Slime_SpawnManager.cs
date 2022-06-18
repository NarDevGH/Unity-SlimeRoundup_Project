using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slime_SpawnManager : MonoBehaviour
{
    private const float SLIME_SPAWN_HEIGHT = 0.25f;
    private const float SPAWNPOINT_CHECK_RANGE = 0.1f;

    [SerializeField] private GameObject[] _slimeVariants;
    [SerializeField] private str_PlayableAreaCorners _playableArea;

    #region DataType_Definitions

    [Serializable]
    struct str_PlayableAreaCorners 
    {
        public Transform lowerLeftCorner;
        public Transform upperRightCorner;
    }

    #endregion


    public GameObject SpawnSlime()
    {
        GameObject newSlime = GenerateRandomSlime();

        newSlime.transform.position = RandomSpawnPositionInsidePlayableArea();

        return newSlime;
    }

    public GameObject SpawnSlime(Vector3 spawnPoint)
    {
        GameObject newSlime = GenerateRandomSlime();

        newSlime.transform.position = spawnPoint;

        return newSlime;
    }

    public GameObject SpawnSlime(Vector3 spawnPoint, int variant = 0)
    {
        GameObject newSlime = GenerateSlime(variant);

        newSlime.transform.position = spawnPoint;

        return newSlime;
    }

    public List<GameObject> SpawnSlimes(int ammountToSpawn)
    {
        List<GameObject> newSlimes = new List<GameObject>();

        for (int i = 0; i < ammountToSpawn; i++)
        {
            GameObject newSlime = GenerateRandomSlime();

            newSlime.transform.position = RandomSpawnPositionInsidePlayableArea();

            newSlimes.Add(newSlime);
        }

        return newSlimes;
    }

    public List<GameObject> SpawnSlimes(int ammountToSpawn, Vector3 spawnPoint)
    {
        List<GameObject> newSlimes = new List<GameObject>();

        for (int i = 0; i < ammountToSpawn; i++)
        {
            GameObject newSlime = GenerateRandomSlime();

            newSlime.transform.position = spawnPoint;

            newSlimes.Add(newSlime);
        }

        return newSlimes;
    }


    #region Slime_Generation

    public GameObject GenerateRandomSlime()
    {
        int randSlimeIndex = UnityEngine.Random.Range(0, _slimeVariants.Length);

        return Instantiate(_slimeVariants[randSlimeIndex]);
    }

    public GameObject GenerateSlime(int variant = 0)
    {
        return Instantiate(_slimeVariants[variant]);
    }

    #endregion

    private Vector3 RandomSpawnPositionInsidePlayableArea()
    {
        Vector3 position;

        float xPos, zPos;
        do
        {
            xPos = UnityEngine.Random.Range(_playableArea.lowerLeftCorner.position.x,
                                              _playableArea.upperRightCorner.position.x);

            zPos = UnityEngine.Random.Range(_playableArea.lowerLeftCorner.position.z,
                                                  _playableArea.upperRightCorner.position.z);

            position = new Vector3(xPos, SLIME_SPAWN_HEIGHT, zPos);

        } while (PositionInsideObject(position) || !PositionOverGround(position) );

        return position;
    }

    private bool PositionInsideObject(Vector3 position) 
    {
        return Physics.CheckSphere(position, SPAWNPOINT_CHECK_RANGE);
    }

    private bool PositionOverGround(Vector3 position) 
    {
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit)) 
        {
            var stufsInGround = Physics.OverlapSphere(position, 1f);
            var evalGroundPos = stufsInGround.Where(x=>x.tag!="Ground").ToList();
            if (evalGroundPos.Count == 0) 
            {
                return true;
            }
        }
        return false;
    }
}
