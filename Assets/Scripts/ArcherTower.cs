using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    private void Start()
    {
        name = "�ü�Ÿ��";
        attackDelay = 0.5f;
        bulletType = E_PoolType.Arrow;
    }
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO : ���Ͱ� Ʈ���ſ� ������ ���� �ڷ�ƾ ȣ��
        if (other.gameObject.GetComponent<Test>())
        {
            Debug.Log("�ü�����");
            Debug.Log($"�ü�{isAttack}");
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
