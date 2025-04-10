using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Bullet
{
    private void Start()
    {
        poolType = E_PoolType.Arrow;
        damage = 1;
    }
    private void OnEnable()
    {
        StartCoroutine(DeleteRoutine());
    }

    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 10);
        transform.LookAt(targetPos);
    }
    IEnumerator DeleteRoutine()
    {
        targetPos = Vector3.zero;
        yield return new WaitForSeconds(3);
        
        ObjectPoolManager.Instance.ReturnObject(this.poolType, gameObject);
    }
}
