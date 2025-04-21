using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform towerSocket;
    [SerializeField] private TowerManagementUI towerManagementUI;
    private bool isEmissionOn;
    public bool isSpawned;
    private Transform gfx;

    private void Awake()
    {
        towerSocket = transform.GetChild(0);
        gfx = transform.GetChild(1);
        towerManagementUI = GameObject.FindGameObjectWithTag("TowerUI").GetComponent<TowerManagementUI>();
    }

    private void Update()
    {
        if (isEmissionOn && Input.GetMouseButtonDown(0) &&
            !RectTransformUtility.RectangleContainsScreenPoint(towerManagementUI.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            isEmissionOn = false;
            SetEmission(false);
            towerManagementUI.TowerBase = null;
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        isEmissionOn = !isEmissionOn;
        SetEmission(isEmissionOn);
        towerManagementUI.TowerBase = isEmissionOn ? this : null;
    }

    private void SetEmission(bool enable)
    {
        foreach (MeshRenderer renderer in gfx.GetComponentsInChildren<MeshRenderer>())
        {
            if (enable)
                renderer.material.EnableKeyword("_EMISSION");
            else
                renderer.material.DisableKeyword("_EMISSION");
        }
    }
}
