using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CardCSV : MonoBehaviour
{
    private static string filePath = "/CSV/CardCSVFile.csv";
    // Unity ��� �޴��� ��ư�� �����ϰ� ��ư�� ������ ����� �ϴ� ���
    [MenuItem("Utilities/Generate Cards")]
    public static void GenerateCards()
    {
        string[] csvText = File.ReadAllLines(Application.dataPath + filePath);
        CardSO cData = ScriptableObject.CreateInstance<CardSO>();

        for (int i = 1; i < csvText.Length; i++)
        {
            string[] stats = csvText[i].Split(',');

            CardData cardData = new CardData();

            cardData.CardNum = int.Parse(stats[0]);
            cardData.CardName = stats[1];
            cardData.Cost = int.Parse(stats[2]);
            cardData.Description = stats[3];
            cardData.CardPrefab = Resources.Load<GameObject>(stats[4]);

            cData.CData.Add(cardData);
        }
        AssetDatabase.CreateAsset(cData, "Assets/SO/CardData.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
