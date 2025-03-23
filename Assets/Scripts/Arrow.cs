using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Bullet
{
    private void Update()
    {
        Shot();
    }
    public override void Shot()
    {
        transform.position = Vector3.MoveTowards(transform.position, Tower.enemy.transform.position, Time.deltaTime * 3);
        transform.LookAt(Tower.enemy.transform);
    }
}
