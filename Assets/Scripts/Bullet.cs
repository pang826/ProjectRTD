using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Tower Tower;
    protected E_PoolType bulletType;
    public abstract void Move();

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ªË¡¶");
        ObjectPoolManager.Instance.ReturnObject(bulletType, gameObject);
    }
}
