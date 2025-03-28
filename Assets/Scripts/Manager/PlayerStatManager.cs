using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;
    private int hp = 10;
    public int Hp {  get { return hp; } }
    private int mp = 20;
    public int Mp { get { return mp; }  }
    private int maxMp = 100;
    public int MaxMp { get { return maxMp; } }
    public int mpBoost = 1;
    private float curTime = 0;

    public UnityAction OnChangeMP;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(mp < MaxMp)
        {
            curTime += Time.deltaTime;
            if (curTime >= 1)
            {
                mp++;
                curTime = 0;
                OnChangeMP?.Invoke();
            }
        }
    }

    public void ConsumeMp()
    {
        if(mp >= TowerSpawnManager.Instance.TowerPrice)
        {
            mp -= TowerSpawnManager.Instance.TowerPrice;
            OnChangeMP.Invoke();
        }
        else
        {
            Debug.LogError("마나가 부족합니다");
        }
    }
}
