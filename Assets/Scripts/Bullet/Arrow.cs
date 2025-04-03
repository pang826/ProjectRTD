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
        targetPos = Tower.enemy.transform.position;
    }
    private void OnEnable()
    {
        if (targetPos == null)
            targetPos = Tower.enemy.transform.position;
    }
    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5);
        transform.LookAt(targetPos);
        StartCoroutine(DeleteRoutine());
    }
    IEnumerator DeleteRoutine()
    {
        yield return new WaitForSeconds(3);
        ObjectPoolManager.Instance.ReturnObject(poolType, gameObject);
    }
}
