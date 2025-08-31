using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    private Button stage1;
    private Button stage2;
    private Button stage3;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SelectStage")
        {
            SoundManager.Instance.StartMainBGM();

            stage1 = transform.GetChild(0).GetComponent<Button>();
            stage2 = transform.GetChild(1).GetComponent<Button>();
            stage3 = transform.GetChild(2).GetComponent<Button>();
            stage1.onClick.AddListener(() => SceneChanger.Instance.SelectStage(1));
            stage2.onClick.AddListener(() => SceneChanger.Instance.SelectStage(2));
            stage3.onClick.AddListener(() => SceneChanger.Instance.SelectStage(3));

            // 스테이지 UI 잠금이 확인 가능하도록
            switch(GameManager.Instance.Stage) 
            {
                case 1:
                    stage2.image.color = Color.gray;
                    stage3.image.color = Color.gray;
                    stage2.enabled = false;
                    stage3.enabled = false;
                    break;
                case 2:
                    stage3.image.color = Color.gray;
                    stage3.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
