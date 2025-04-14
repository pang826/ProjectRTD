using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : Card
{
    private void Awake()
    {
        radius = 3;
    }
    public override void Active()
    {
        throw new System.NotImplementedException();
    }
}
