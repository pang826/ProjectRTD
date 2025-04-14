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
            Debug.Log("ī�� ���� ���콺 ����");
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
        Debug.Log("Ŭ��");
    }
    public virtual void OnDrag(PointerEventData data)
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

    public virtual void OnEndDrag(PointerEventData data)
    {
        // ī�� UI / Ÿ�� UI ������ ��� ���� ���������� ��ġ ����
        if (RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            transform.position = startPos;
        }
    }
}
