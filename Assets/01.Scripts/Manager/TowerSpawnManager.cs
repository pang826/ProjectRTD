using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnManager : MonoBehaviour
{
    public static TowerSpawnManager Instance;
    [SerializeField] private GameObject[] towers;
    Dictionary<E_TowerType, Queue<GameObject>> towerDic = new Dictionary<E_TowerType, Queue<GameObject>>();
    private int towerCount;
    [SerializeField] public int TowerPrice;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        towerCount = towers.Length;

        for(int i = 0; i < towers.Length; i++) 
        {
            Queue<GameObject> que = new Queue<GameObject>();
            towerDic.Add((E_TowerType)i, que);
        }
    }

    public GameObject Spawn()
    {
        int randNum = Random.Range(0, towerCount);
        towerDic[(E_TowerType)randNum].Enqueue(towers[randNum]);
        return towers[randNum];
    }

    public void UpgradeTower(Tower tower)
    {
        tower.UpgradeTower(tower.Lv2Dmg, tower.Lv3Dmg);
    }
}
