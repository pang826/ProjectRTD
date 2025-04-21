using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private TrailRenderer followTrail;
    [SerializeField] private ParticleSystem clickParticle;

    private Vector3 mousePos;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        followTrail.transform.position = worldPos;
    }
}
