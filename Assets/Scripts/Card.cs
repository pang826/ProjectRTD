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
        Debug.Log("클릭");
        
    }
    public void OnDrag(PointerEventData data)
    {
        Debug.Log("드래그");
        transform.position = data.position;
        // 카드 UI / 타워 UI 영역이 아닐때 조건
        if(!RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || !RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("카드UI 영역이 아님");
        }
        
    }

    public void OnEndDrag(PointerEventData data)
    {
        // 카드 UI / 타워 UI 영역일 경우 원래 포지션으로 위치 변경
        if (RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            transform.position = startPos;
        }
    }
}
