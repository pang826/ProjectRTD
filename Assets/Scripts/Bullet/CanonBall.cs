using System.Collections;
using UnityEngine;

public class CanonBall : Bullet
{
    private void Start()
    {
        poolType = E_PoolType.MagicBall;
        damage = 5;
        targetPos = Tower.enemy.transform.position;
    }
    private void OnEnable()
    {
        if (targetPos == Vector3.zero && gameObject != null)
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
        targetPos = Vector3.zero;
        ObjectPoolManager.Instance.ReturnObject(poolType, gameObject);
    }
}
