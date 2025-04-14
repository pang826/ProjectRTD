using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCard : Card
{
    private void Awake()
    {
        radius = 10;
    }
    public override void Active()
    {
        throw new System.NotImplementedException();
    }
}
