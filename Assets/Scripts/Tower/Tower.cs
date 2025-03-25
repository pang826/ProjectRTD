using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected Collider[] enemys;
    public GameObject enemy;
    protected E_PoolType poolType;
    protected bool isAttack;
    protected float attackDelay;
    protected float range;
    public float Damage;
    public virtual void Attack(Tower child)
    {
        if(child.isAttack == false)
        StartCoroutine(AttackRoutine(child));
    }

    public virtual IEnumerator AttackRoutine(Tower tower)
    {
        tower.isAttack = true;
        yield return new WaitForSeconds(tower.attackDelay);
        tower.isAttack = false;
        yield break;
    }
}
