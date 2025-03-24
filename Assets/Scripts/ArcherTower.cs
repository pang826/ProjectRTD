using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherTower : Tower
{
    private void Start()
    {
        name = "궁수타워";
        attackDelay = 0.5f;
        poolType = E_PoolType.Arrow;
    }
    private void Update()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, 3f, 1 << 3);
        if (enemys.Length > 0)
        {
            enemy = enemys[0].gameObject;
            Attack(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO : 몬스터가 트리거에 있으면 공격 코루틴 호출
        if (other.gameObject.GetComponent<Test>())
        {
            enemy = other.gameObject;
            Attack(this);
        }
    }

    public override void Attack(Tower child)
    {
        if (isAttack == false)
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject(poolType, transform);
            if(obj.GetComponent<Bullet>().Tower == null)
            {
                obj.GetComponent<Bullet>().Tower = child;
            }
        }
        base.Attack(child);
    }
}
