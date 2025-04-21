using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCard : Card
{
    private void Awake()
    {
        cost = 5;
        radius = 3;
    }
    public override void Active()
    {
        if (PlayerStatManager.Instance.Mp >= cost)
        {
            PlayerStatManager.Instance.ConsumeMP(cost);
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
}
