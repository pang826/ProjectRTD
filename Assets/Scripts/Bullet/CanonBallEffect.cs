using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallEffect : MonoBehaviour
{
    float time = 0.3f;
    float curTime = 0;
    private void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= time) 
        {
            curTime = 0;
            ObjectPoolManager.Instance.ReturnObject(E_PoolType.CanonBallEffect, gameObject);
        }
    }
}
