using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicTower : Tower
{
    void Start()
    {
        name = "����Ÿ��";
        attackDelay = 1;
        poolType = E_PoolType.MagicBall;
        range = 8f;
    }
    private void Update()
    {
        enemys = Physics.OverlapSphere(transform.position, range, 1 << 3);
        if (enemys.Length > 0)
        {
            enemys = enemys.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToArray();
            // �Ÿ��� ����� ���� �迭 ù��° ������ �����ϸ鼭 ȭ���� Ÿ������ ����
            enemy = enemys[0].gameObject;
            transform.LookAt(enemy.transform.position);
            Attack(this);
        }
    }
    public override void Attack(Tower child)
    {
        if (isAttack == false)
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject(child.poolType, transform);
            obj.GetComponent<Bullet>().Init(child, enemy.transform.position);
        }
        base.Attack(child);
    }
}
