using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Bullet
{
    private void Start()
    {
        poolType = E_PoolType.MagicBall;
    }
    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Tower.enemy.transform.position, Time.deltaTime * 5);
        transform.LookAt(Tower.enemy.transform);
    }
}
