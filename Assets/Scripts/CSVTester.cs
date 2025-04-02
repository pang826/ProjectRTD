using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CSVTester : MonoBehaviour
{
    private static string filePath = "/CSV/TestCSVFile.csv";
    // Unity ��� �޴��� ��ư�� �����ϰ� ��ư�� ������ ����� �ϴ� ���
    [MenuItem("Utilities/Generate Enemies")]
    public static void GenerateEnemies()
    {
        string[] csvText = File.ReadAllLines(Application.dataPath + filePath);
        Monster mData = ScriptableObject.CreateInstance<Monster>();

        //foreach(string csv in csvText)
        //{
        //    string[] stats = csv.Split(',');
        //    if(stats.Length != 3)
        //    {
        //        Debug.LogError("�����Ͱ� 3�� ����");
        //    }
        //
        //    MonsterData monsterData = new MonsterData();
        //
        //    monsterData.Name = stats[0];
        //    monsterData.Hp = int.Parse(stats[1]);
        //    monsterData.Speed = float.Parse(stats[2]);
        //
        //    mData.MData.Add(monsterData);
        //}

        for(int i = 1; i < csvText.Length; i++)
        {
            string[] stats = csvText[i].Split(",");

            MonsterData monsterData = new MonsterData();

            monsterData.Name = stats[0];
            monsterData.Hp = int.Parse(stats[1]);
            monsterData.Speed = float.Parse(stats[2]);

            mData.MData.Add(monsterData);
        }
        AssetDatabase.CreateAsset(mData, "Assets/SO/MonsterData.asset");
        AssetDatabase.SaveAssets();
    }
}
