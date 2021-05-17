using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlimesManager))]
public class SpawnAtStart : MonoBehaviour
{
    [SerializeField] private SlimesManager _slimesManager;
    [SerializeField] private int ammountToSpawn = 1;
    [SerializeField] private Transform centerSpawnPoint;
    [SerializeField] private Vector2 spawnDistanceRange;

    void Start()
    {
        _slimesManager.SpawnSlimes(ammountToSpawn,centerSpawnPoint.position,spawnDistanceRange);
    }

    

}
