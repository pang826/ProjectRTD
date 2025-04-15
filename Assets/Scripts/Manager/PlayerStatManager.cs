using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;
    private int hp = 10;
    public int Hp {  get { return hp; } }
    private int maxHp = 10;
    public int MaxHp { get { return maxHp; } }

    private int energy = 30;
    public int Energy { get { return energy; }  }
    private int maxEnergy = 100;
    public int MaxEnergy { get { return maxEnergy; } }

    private int mp = 0;
    public int Mp { get { return mp; } }
    private int maxMp = 20;
    public int MaxMp { get { return maxMp; } }

    public int mpBoost = 1;
    private float curEnergyTime = 0;
    private float curMpTime = 0;

    public UnityAction OnChangeHp;
    public UnityAction OnChangeEnergy;
    public UnityAction OnChangeMP;
    public UnityAction OnAttachEndPos;              // 적 목표지점 도착

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
        OnAttachEndPos += DecreaseHp;
    }

    private void Update()
    {
        if(energy < MaxEnergy)
        {
            curEnergyTime += Time.deltaTime;
            if (curEnergyTime >= 1)
            {
                energy++;
                curEnergyTime = 0;
                OnChangeEnergy?.Invoke();
            }
        }
    }

    public void ConsumeEnergy()
    {
        if(energy >= TowerSpawnManager.Instance.TowerPrice)
        {
            energy -= TowerSpawnManager.Instance.TowerPrice;
            OnChangeEnergy?.Invoke();
        }
        else
        {
            Debug.LogError("에너지가 부족합니다");
        }
    }
    public void GetMp()
    {
        mp++;
        OnChangeMP?.Invoke();
    }

    public void ConsumeMP(int cost)
    {
        if(mp >= cost)
        {
            mp -= cost;
            OnChangeMP?.Invoke();
        }
    }

    public void DecreaseHp()
    {
        hp--;
        OnChangeHp?.Invoke();
        if(hp <= 0)
        {
            GameManager.Instance.OnStageDefeat?.Invoke();
        }
    }
}
