using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] protected Bullet bullet;
    protected E_PoolType bulletType;
    protected bool isAttack;
    protected float attackDelay;
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
