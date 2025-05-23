using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("변수")]
    [SerializeField]
    private int stage = 1;
    public int Stage { get { return stage; } }
    private int round = 1;
    public int Round { get { return round; } }
    private int bossRound = 5;          // 보스 라운드
    public int BossRound { get {  return bossRound; } }
    [SerializeField]
    private int monsterCount;
    public int MonsterCount { get {  return monsterCount; } }
    private int mCount;
    [SerializeField]
    private int curMonsterCount;
    public int CurMonsterCount { get { return curMonsterCount; } }
    public TextMeshProUGUI ClearTMP;
    public TextMeshProUGUI DefeatTMP;

    public bool IsClear;                // 클리어 여부


    public UnityAction OnChangeStage;
    public UnityAction OnIncreaseRound;             // 라운드 상승
    public UnityAction OnChangeCurMonsterCount;     // 현재 몬스터 수 변화
    public UnityAction OnStageClear;                // 현재 스테이지 클리어
    public UnityAction OnStageDefeat;               // 현재 스테이지 실패
    

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
        IsClear = true;
        curMonsterCount = monsterCount;
        mCount = monsterCount;
        OnIncreaseRound += IncreaseRound;
        OnChangeCurMonsterCount += ChangeCurMonsterCount;
        OnStageClear += Clear;
        OnStageDefeat += Defeat;
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void IncreaseRound() 
    {
        if (PlayerStatManager.Instance.Hp <= 0) return;
        round++;
        if (round > bossRound)      // 보스라운드 종료 시 스테이지 종료 이벤트 호출
        {
            switch(IsClear)
            {
                case true:
                    OnStageClear.Invoke();
                    return;
                case false:
                    OnStageDefeat.Invoke(); 
                    return;
            }
        }
            
        if (round != bossRound)
        {
            monsterCount = mCount;
            curMonsterCount = mCount;
        }
        else if(round == bossRound)
        { 
            monsterCount = 1;
            curMonsterCount = 1;
        }
    }
    private void ChangeCurMonsterCount() 
    { 
        curMonsterCount--;
        if(curMonsterCount == 0) 
        {
            StartCoroutine(StartRoundRoutine());
        }
    }

    IEnumerator StartRoundRoutine()
    {
        yield return new WaitForSeconds(5);
        OnIncreaseRound.Invoke();
    }
    
    private void Clear()
    {
        ClearTMP.gameObject.SetActive(true);
    }

    private void Defeat()
    {
        DefeatTMP.gameObject.SetActive(true);
    }
}
