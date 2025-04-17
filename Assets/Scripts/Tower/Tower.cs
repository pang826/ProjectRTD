using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected Collider[] enemys;
    public GameObject enemy;
    public E_PoolType poolType;
    protected bool isAttack;
    protected float attackDelay;
    protected float range;
    public int Damage;
    public int Lv2Dmg;
    public int Lv3Dmg;

    [SerializeField] protected ParticleSystem lv2Effect;
    [SerializeField] protected ParticleSystem lv3Effect;
    [SerializeField] protected Animator anim;

    [SerializeField] protected int towerLv = 1;
    public virtual void Attack(Tower child)
    {
        if(child.isAttack == false)
        StartCoroutine(AttackRoutine(child));
    }

    public virtual void UpgradeTower(int lv2Dmg, int lv3Dmg)
    {
        switch(towerLv)
        {
            case 1:
                towerLv++;
                Damage += lv2Dmg;
                lv2Effect.gameObject.SetActive(true);
                break;
            case 2:
                towerLv++;
                Damage += lv3Dmg;
                lv2Effect.gameObject.SetActive(false);
                lv3Effect.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public virtual IEnumerator AttackRoutine(Tower tower)
    {
        tower.isAttack = true;
        yield return new WaitForSeconds(tower.attackDelay);
        tower.isAttack = false;
        yield break;
    }
}
