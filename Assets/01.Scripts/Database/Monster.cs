using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ScriptableObject
{
    public List<MonsterData> MData = new List<MonsterData>();
}

[Serializable]
public class MonsterData
{
    public string Name;
    public int Hp;
    public float Speed;
    public GameObject Prefab;
}
