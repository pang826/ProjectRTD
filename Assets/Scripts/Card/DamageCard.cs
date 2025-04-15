using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCard : Card
{
    private void Awake()
    {
        radius = 3;
    }
    public override void Active()
    {
        Collider[] colliders = Physics.OverlapSphere(curCircleObj.transform.position, radius, 1 << 3);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Unit>(out Unit unit))
            {
                unit.TakeDamage(5);
            }
        }
    }
}
