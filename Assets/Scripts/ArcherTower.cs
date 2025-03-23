using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    private void Start()
    {
        name = "궁수타워";
        attackDelay = 0.5f;
        bulletType = E_PoolType.Arrow;
    }
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO : 몬스터가 트리거에 있으면 공격 코루틴 호출
        if (other.gameObject.GetComponent<Test>())
        {
            Debug.Log("궁수접촉");
            Debug.Log($"궁수{isAttack}");
            enemy = other.gameObject;
            Attack(this);
        }
    }

    public override void Attack(Tower child)
    {
        if (isAttack == false)
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject(bulletType, transform);
            obj.GetComponent<Bullet>().Tower = child;
        }
        base.Attack(child);
    }
}
