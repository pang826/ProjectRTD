using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector2 startPos;
    private GameObject cardUI;
    private GameObject towerUI;
    
    private void Start()
    {
        cardUI = GameObject.FindGameObjectWithTag("CardUI");
        towerUI = GameObject.FindGameObjectWithTag("TowerUI");
        startPos = transform.position;
    }
    public void OnPointerClick(PointerEventData data)
    {
        
    }
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Ŭ��");
        
    }
    public void OnDrag(PointerEventData data)
    {
        Debug.Log("�巡��");
        transform.position = data.position;
        // ī�� UI / Ÿ�� UI ������ �ƴҶ� ����
        if(!RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || !RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("ī��UI ������ �ƴ�");
        }
        
    }

    public void OnEndDrag(PointerEventData data)
    {
        // ī�� UI / Ÿ�� UI ������ ��� ���� ���������� ��ġ ����
        if (RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            transform.position = startPos;
        }
    }
}
