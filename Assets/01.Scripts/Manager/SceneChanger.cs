using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

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
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SelectStage");
    }

    public void SelectStage(int num)
    {
        if(GameManager.Instance.Stage <= num)
        {
            SceneManager.LoadScene($"Stage{num}");
            GameManager.Instance.CurStage = num;        // ���ӸŴ����� ���� �������� �� �ο�
        }
    }

    public void ChangeToSelectScene()
    {
        SceneManager.LoadScene("SelectStage");
    }
}
