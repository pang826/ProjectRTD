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
    public int mpBoost = 1;
    private float curTime = 0;

    public UnityAction OnChangeHp;
    public UnityAction OnChangeEnergy;
    public UnityAction OnChange;
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
            curTime += Time.deltaTime;
            if (curTime >= 1)
            {
                energy++;
                curTime = 0;
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
