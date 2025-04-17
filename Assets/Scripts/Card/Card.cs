using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] protected string cardName;
    [SerializeField] protected int cost;
    [SerializeField] protected string description;

    private GameObject cardUI;
    private TextMeshProUGUI tmp;
    private GameObject towerUI;
    private Image image;

    [SerializeField] protected GameObject circleAreaObj;
    protected GameObject curCircleObj;

    private Vector2 startPos;
    [SerializeField] protected float radius;
    
    private void Start()
    {
        cardUI = GameObject.FindGameObjectWithTag("CardUI");
        tmp = cardUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        towerUI = GameObject.FindGameObjectWithTag("TowerUI");
        startPos = transform.position;
        image = GetComponent<Image>();
        tmp.text = description;
    }

    private void OnDisable()
    {
        tmp.text = null;
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
        
    }

    public virtual void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
        // 카드 UI / 타워 UI 영역이 아닐때 조건
        if(RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition) == false
            && RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition) == false)
        {
            Debug.Log("카드UI, 타워UI 영역이 아님");
            DrawCircleArea(data.position);
            if (image.enabled == true)
            {
                image.enabled = false;
            }
        }
        // 카드 UI / 타워 UI 일 경우
        else
        {
            if(image.enabled == false)
            {
                image.enabled = true;
            }
            if (curCircleObj != null)
            {
                Destroy(curCircleObj);
                curCircleObj = null;
            }
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
        // 게임 화면 영역일 때
        else
        {
            Active();
            Destroy(gameObject);
            if(curCircleObj != null) 
            {
                Destroy(curCircleObj);
                curCircleObj = null;
            }
        }
    }

    private void DrawCircleArea(Vector3 pos)
    {
        if(curCircleObj == null) 
        {
            curCircleObj = Instantiate(circleAreaObj);
        }
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        // "Ground" 레이어에만 맞도록 하고 싶다면 LayerMask 사용도 가능
        if (Physics.Raycast(ray, out hit, 100f, 1 << 9))
        {
            curCircleObj.transform.position = hit.point;
            curCircleObj.transform.localScale = Vector3.one * radius * 2f;
        }
    }
}
