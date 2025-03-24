using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnManager : MonoBehaviour
{
    public static TowerSpawnManager Instance;
    [SerializeField] private GameObject[] towers;
    private int towerCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
        towerCount = towers.Length;
    }

    public GameObject Spawn()
    {
        int randNum = Random.Range(0, towerCount);
        return towers[randNum];
    }
}
