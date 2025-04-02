using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster mData;
    public Transform spawnPos;
    public float spawnInterval;
    private int wave = 1;
    public int spawnCount;
    private int curCount;
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while(curCount < spawnCount)
        {
            SpawnMonster(mData.MData[wave].Name, spawnPos.position);
            curCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
        Debug.Log("웨이브 종료");
        curCount = 0;
        wave++;
        yield break;
    }
    public void SpawnMonster(string name, Vector3 pos)
    {
        MonsterData monsterData = mData.MData.Find(e => e.Name == name);
        
        GameObject monster = Instantiate(monsterData.Prefab, pos, Quaternion.identity);
    }
}
