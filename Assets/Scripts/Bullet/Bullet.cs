using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Tower Tower;
    [SerializeField] protected Vector3 targetPos;
    protected E_PoolType poolType;
    protected int damage;
    public abstract void Move();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
            Debug.Log("ªË¡¶");
            ObjectPoolManager.Instance.ReturnObject(poolType, gameObject);
        }
    }
}
