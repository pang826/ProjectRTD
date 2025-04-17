using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCard : Card
{
    private void Awake()
    {
        cost = 3;
        radius = 5;
    }
    public override void Active()
    {
        if(PlayerStatManager.Instance.Mp >= cost)
        {
            PlayerStatManager.Instance.ConsumeMP(cost);
            Collider[] colliders = Physics.OverlapSphere(curCircleObj.transform.position, radius, 1 << 3);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.SetSpeedEffect(0.7f, 3);
                }
            }
        }
    }
}
