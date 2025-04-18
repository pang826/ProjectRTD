using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private ParticleSystem followParticle;
    [SerializeField] private ParticleSystem clickParticle;

    private Vector3 mousePos;
    private void Update()
    {
        mousePos = Input.mousePosition;
    }
}
