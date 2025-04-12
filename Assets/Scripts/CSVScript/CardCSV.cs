using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CardCSV : MonoBehaviour
{
    private static string filePath = "/CSV/CardCSVFile.csv";
    // Unity 상단 메뉴에 버튼을 생성하고 버튼을 누르면 기능을 하는 방법
    [MenuItem("Utilities/Generate Cards")]
    public static void GenerateCards()
    {
        string[] csvText = File.ReadAllLines(Application.dataPath + filePath);
        CardSO cData = ScriptableObject.CreateInstance<CardSO>();

        for (int i = 1; i < csvText.Length; i++)
        {
            string[] stats = csvText[i].Split(',');

            CardData cardData = new CardData();

            cardData.CardName = stats[0];
            cardData.Cost = int.Parse(stats[1]);
            cardData.Description = stats[2];
            cardData.CardPrefab = Resources.Load<GameObject>(stats[3]);

            cData.CData.Add(cardData);
        }
        AssetDatabase.CreateAsset(cData, "Assets/SO/CardData.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
