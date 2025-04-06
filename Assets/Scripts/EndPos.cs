using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if(other.gameObject.TryGetComponent<Boss>(out Boss boss))
            {
                Debug.Log("보스 도달");
                GameManager.Instance.IsClear = false;
            }
            other.GetComponent<Unit>().AttachEndPos();
        }
    }
}
