using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public CardSO cardSO;

    public Dictionary<string, GameObject> cardDictionary = new Dictionary<string, GameObject>();

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
        cardSocket = new Vector2(-625, 0);
    }

    private void Start()
    {
        foreach(CardData cardSo in cardSO.CData)
        {
            Card card = cardSo.CardPrefab.GetComponent<Card>();
            card.Init(cardSo);
            cardDictionary[cardSo.CardName] = cardSo.CardPrefab;
        }
    }

    public void SpawnCard()
    {
        GameObject obj = Instantiate(cardDictionary["SlowCard"], cardSocket, Quaternion.identity);
        obj.transform.parent = cardTransform;
    }
}
