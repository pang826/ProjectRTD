using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanonTower : Tower
{
    void Start()
    {
        anim = GetComponent<Animator>();
        name = "캐논타워";
        attackDelay = 3;
        poolType = E_PoolType.CanonBall;
        range = 8f;
        Damage = 7;
    }
    private void Update()
    {
        enemys = Physics.OverlapSphere(transform.position, range, 1 << 3);
        if (enemys.Length > 0)
        {
            enemys = enemys.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToArray();
            // 거리가 가까운 것을 배열 첫번째 값으로 변경하면서 화살의 타겟으로 설정
            enemy = enemys[0].gameObject;
            transform.LookAt(enemy.transform.position);
            Attack(this);
        }
    }
    public override void UpgradeTower(int lv2Dmg, int lv3Dmg)
    {
        base.UpgradeTower(lv2Dmg, lv3Dmg);
    }
    public override void Attack(Tower child)
    {
        if (isAttack == false)
        {
            SoundManager.Instance.StartSFX(SoundManager.E_SFX.Canon);
            anim.SetTrigger("isAttack");
            GameObject obj = ObjectPoolManager.Instance.GetObject(child.poolType, transform);
            obj.GetComponent<Bullet>().Init(child, enemy.transform.position);
        }
        base.Attack(child);
    }
}
