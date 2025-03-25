using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Bullet
{
    private void Start()
    {
        poolType = E_PoolType.Arrow;
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
