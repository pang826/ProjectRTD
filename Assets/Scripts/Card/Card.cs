using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] protected string cardName;
    [SerializeField] protected int cost;
    [SerializeField] protected string description;

    [SerializeField] private Vector2 startPos;
    [SerializeField] private GameObject cardUI;
    [SerializeField] private GameObject towerUI;
    
    private void Start()
    {
        cardUI = GameObject.FindGameObjectWithTag("CardUI");
        towerUI = GameObject.FindGameObjectWithTag("TowerUI");
        startPos = transform.position;
    }

    public abstract void Active();

    private void Update()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("카드 위에 마우스 있음");
        }
    }

    public void Init(CardData data)
    {
        cardName = data.CardName;
        cost = data.Cost;
        description = data.Description;
    }
    public virtual void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("클릭");
    }
    public virtual void OnDrag(PointerEventData data)
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

    public virtual void OnEndDrag(PointerEventData data)
    {
        // 카드 UI / 타워 UI 영역일 경우 원래 포지션으로 위치 변경
        if (RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            transform.position = startPos;
        }
    }
}
