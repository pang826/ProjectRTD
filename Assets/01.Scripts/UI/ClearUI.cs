using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnClickSetting()
    {
        SceneChanger.Instance.ChangeToSelectScene();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Stage"))
        {
            Button button = transform.GetChild(1).GetComponent<Button>();
            button.onClick.AddListener(OnClickSetting);
        }
    }
}
