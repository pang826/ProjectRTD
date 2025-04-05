using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanonTower : Tower
{
    void Start()
    {
        name = "ĳ��Ÿ��";
        attackDelay = 2;
        poolType = E_PoolType.CanonBall;
        range = 8f;
        Damage = 1f;
    }
    private void Update()
    {
        enemys = Physics.OverlapSphere(transform.position, range, 1 << 3);
        if (enemys.Length > 0)
        {
            enemys = enemys.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToArray();
            // �Ÿ��� ����� ���� �迭 ù��° ������ �����ϸ鼭 ȭ���� Ÿ������ ����
            enemy = enemys[0].gameObject;
            Attack(this);
        }
    }
    public override void Attack(Tower child)
    {
        if (isAttack == false)
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject(this.poolType, transform);
            obj.GetComponent<Bullet>().Tower = child;
        }
        base.Attack(child);
    }
}
