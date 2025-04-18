using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSO : ScriptableObject
{
    public List<CardData> CData = new List<CardData>();
}

[Serializable]
public class CardData
{
    public int CardNum;
    public string CardName;
    public int Cost;
    public string Description;
    public GameObject CardPrefab;
}
