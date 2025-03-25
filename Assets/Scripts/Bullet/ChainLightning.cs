using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : Bullet
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
        transform.position = Vector3.MoveTowards(transform.position, Tower.enemy.transform.position, Time.deltaTime * 10);
        transform.LookAt(Tower.enemy.transform);
    }
}
