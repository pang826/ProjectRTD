using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Tower Tower;
    protected E_PoolType poolType;
    public abstract void Move();

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("ªË¡¶");    
        ObjectPoolManager.Instance.ReturnObject(poolType, gameObject);
    }
}
