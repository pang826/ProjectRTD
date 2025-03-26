using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] private Transform towerSocket;
    [SerializeField] private TowerManagementUI towerManagementUI;
    private bool isEmissionOn;
    public bool isSpawned;
    private void Awake()
    {
        towerSocket = transform.GetChild(0);
        towerManagementUI = GameObject.FindGameObjectWithTag("TowerUI").GetComponent<TowerManagementUI>();
    }
    private void Update()
    {
        if(isEmissionOn == true && Input.GetMouseButtonDown(0) &&
            RectTransformUtility.RectangleContainsScreenPoint(towerManagementUI.gameObject.GetComponent<RectTransform>(), Input.mousePosition) == false)
        {
            isEmissionOn = false;
            GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            towerManagementUI.TowerBase = null;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Å¬¸¯");
        if (isEmissionOn == false)
        {
            isEmissionOn = true;
            GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            towerManagementUI.TowerBase = this;
        }
        else if(isEmissionOn == true)
        {
            isEmissionOn = false;
            GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            towerManagementUI.TowerBase = null;
        }
    }
}
