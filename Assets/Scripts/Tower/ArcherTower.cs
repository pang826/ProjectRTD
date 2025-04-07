using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherTower : Tower
{
    private void Start()
    {
        name = "�ü�Ÿ��";
        attackDelay = 0.5f;
        poolType = E_PoolType.Arrow;
        range = 10f;
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
        if(enemy != null)
        transform.LookAt(enemy.transform.position);
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
