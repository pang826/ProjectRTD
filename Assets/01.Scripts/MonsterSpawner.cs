using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster mData;
    public Transform spawnPos;
    public float spawnInterval;
    private int wave;
    private int curCount;
    private bool isSpawn;
    

    private void Start()
    {
        wave = GameManager.Instance.Round - 1;
        curCount = GameManager.Instance.MonsterCount;
        StartCoroutine(SpawnRoutine());
        GameManager.Instance.OnIncreaseRound += ChangeMCount;
    }

    private void ChangeMCount()
    {
        if (GameManager.Instance.Round > GameManager.Instance.BossRound ||
            PlayerStatManager.Instance.Hp <= 0) return;
        wave = GameManager.Instance.Round - 1;
        curCount = GameManager.Instance.MonsterCount;
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(5f);
        while(curCount > 0 && isSpawn == false)
        {
            isSpawn = true;
            SpawnMonster(wave, spawnPos.position);
            curCount--;
            yield return new WaitForSeconds(spawnInterval);
            isSpawn = false;
        }
        Debug.Log("웨이브 종료");
        yield break;
    }
    public void SpawnMonster(int num, Vector3 pos)
    {
        List<MonsterData> monsterData = mData.MData;
        
        GameObject monster = Instantiate(monsterData[num].Prefab, pos, Quaternion.identity);
        monster.GetComponent<Unit>().SetData(monsterData[num]);
    }
}
