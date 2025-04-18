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
        
    }

    public virtual void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
        // ī�� UI / Ÿ�� UI ������ �ƴҶ� ����
        if(RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition) == false
            && RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition) == false)
        {
            Debug.Log("ī��UI, Ÿ��UI ������ �ƴ�");
            DrawCircleArea(data.position);
            if (image.enabled == true)
            {
                image.enabled = false;
            }
        }
        // ī�� UI / Ÿ�� UI �� ���
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
        // ī�� UI / Ÿ�� UI ������ ��� ���� ���������� ��ġ ����
        if (RectTransformUtility.RectangleContainsScreenPoint(cardUI.GetComponent<RectTransform>(), Input.mousePosition)
            || RectTransformUtility.RectangleContainsScreenPoint(towerUI.GetComponent<RectTransform>(), Input.mousePosition))
        {
            transform.position = startPos;
        }
        // ���� ȭ�� ������ ��
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

        // "Ground" ���̾�� �µ��� �ϰ� �ʹٸ� LayerMask ��뵵 ����
        if (Physics.Raycast(ray, out hit, 100f, 1 << 9))
        {
            curCircleObj.transform.position = hit.point;
            curCircleObj.transform.localScale = Vector3.one * radius * 2f;
        }
    }
}
