using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Bullet
{
    private void Start()
    {
        poolType = E_PoolType.MagicBall;
        damage = 3;
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
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5);
        transform.LookAt(targetPos);
    }
    IEnumerator DeleteRoutine()
    {
        targetPos = Vector3.zero;
        yield return new WaitForSeconds(3);
        
        ObjectPoolManager.Instance.ReturnObject(this.poolType, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            Unit unit = other.gameObject.GetComponent<Unit>();
            unit.SetSpeedEffect(0.5f, 1f);
            base.OnTriggerEnter(other);
        }
    }
}
