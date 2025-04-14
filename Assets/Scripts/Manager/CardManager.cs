using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public CardSO cardSO;

    public Dictionary<int, GameObject> cardDictionary = new Dictionary<int, GameObject>();

    GameObject curObj;

    Transform cardTransform;
    Vector2 cardSocket;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        cardTransform = GameObject.FindGameObjectWithTag("CardUI").transform;
        cardSocket = cardTransform.GetChild(1).transform.position;
    }

    private void Start()
    {
        foreach(CardData cardSo in cardSO.CData)
        {
            Card card = cardSo.CardPrefab.GetComponent<Card>();
            card.Init(cardSo);
            cardDictionary[cardSo.CardNum] = cardSo.CardPrefab;
        }
    }

    public void SpawnCard()
    {
        if (curObj != null) return;
        int randNum = Random.Range(1, cardDictionary.Count + 1);
        GameObject obj = Instantiate(cardDictionary[randNum], cardSocket, Quaternion.identity);
        curObj = obj;
        obj.transform.parent = cardTransform;
    }
}
