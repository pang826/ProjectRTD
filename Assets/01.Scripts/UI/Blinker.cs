using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    [SerializeField] GameObject obj;
    bool isActive;
    private void OnEnable()
    {
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while(true)
        {
            obj.SetActive(isActive);
            yield return new WaitForSeconds(1);
            obj.SetActive(!isActive);
            yield return new WaitForSeconds(1);
            yield return null;
        }
    }
}
