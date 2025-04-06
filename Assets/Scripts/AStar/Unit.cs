using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    public Monster monsterData;
    public Transform target;
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    Vector3[] path;
    private int targetIndex;
    private Coroutine routine;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("EndPos").transform;
        hp = monsterData.MData[GameManager.Instance.Round - 1].Hp;
        speed = monsterData.MData[GameManager.Instance.Round - 1].Speed;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void SetData(MonsterData monsterData)
    {
        hp = monsterData.Hp;
        speed = monsterData.Speed;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            GameManager.Instance.OnChangeCurMonsterCount?.Invoke();
            Destroy(gameObject);
        }
    }

    public void AttachEndPos()
    {
        PlayerStatManager.Instance.OnAttachEndPos?.Invoke();
        GameManager.Instance.OnChangeCurMonsterCount?.Invoke();
        Destroy(gameObject);
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (this == null || gameObject == null) return;

        if (pathSuccessful)
        {
            path = newPath;
            if(routine != null) 
            {
                StopCoroutine(FollowPath());
            }
            
            routine = StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 curWayPoint = path[0];
        while(true)
        {
            if (transform.position == curWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                curWayPoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, curWayPoint, Time.deltaTime * speed);
            transform.LookAt(curWayPoint);
            yield return null;
        }
    }
}
