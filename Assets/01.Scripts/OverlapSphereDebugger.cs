using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int hp = 10;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
    }

    private void Update()
    {
        if(hp <= 0)
        {

        }
    }
}
