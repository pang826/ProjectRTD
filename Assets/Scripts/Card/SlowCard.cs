using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCard : Card
{
    private void Awake()
    {
        radius = 5;
    }
    public override void Active()
    {
        Collider[] colliders = Physics.OverlapSphere(curCircleObj.transform.position, radius, 1 << 3);
        foreach (Collider collider in colliders) 
        {
            if (collider.gameObject.TryGetComponent<Unit>(out Unit unit))
            {
                unit.SetSpeedEffect(0.3f, 3);
            }
        }
    }
}
