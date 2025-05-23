using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Tower Tower;
    [SerializeField] protected Vector3 targetPos;
    protected E_PoolType poolType;
    protected int damage;
    protected float speed;
    private void Awake()
    {
        targetPos = new Vector3(1, 1, 1);
    }

    public virtual void Init(Tower tower, Vector3 targetPos)
    {
        Tower = tower;
        damage = tower.Damage;
        this.targetPos = targetPos;
        speed = tower.Speed;
    }
    public abstract void Move();


    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
            ObjectPoolManager.Instance.ReturnObject(poolType, gameObject);
        }
    }
}
