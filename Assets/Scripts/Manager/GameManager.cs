using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("����")]
    [SerializeField]
    private int stage = 1;
    public int Stage { get { return stage; } }
    [SerializeField]
    private int round = 1;
    public int Round { get { return round; } }
    [SerializeField]
    private int monsterCount;
    public int MonsterCount { get {  return monsterCount; } }
    private int mCount;
    [SerializeField]
    private int curMonsterCount;
    public int CurMonsterCount { get { return curMonsterCount; } }


    public UnityAction OnChangeStage;
    public UnityAction OnIncreaseRound;
    public UnityAction OnChangeMonsterCount;
    public UnityAction OnChangeCurMonsterCount;

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
        curMonsterCount = monsterCount;
        mCount = monsterCount;
    }

    private void Start()
    {
        OnIncreaseRound += IncreaseRound;
        OnChangeCurMonsterCount += ChangeCurMonsterCount;
    }
    private void Update()
    {
    }

    private void IncreaseRound() 
    {
        round++;
        if (round != 5)
        {
            monsterCount = mCount;
            curMonsterCount = mCount;
        }
        else { monsterCount = 1; }
    }
    private void ChangeCurMonsterCount() 
    { 
        curMonsterCount--;
        if(curMonsterCount == 0) 
        {
            OnIncreaseRound.Invoke();
        }
    }
}
