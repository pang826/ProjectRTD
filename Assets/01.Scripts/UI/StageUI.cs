using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    private Button stage1;
    private Button stage2;
    private Button stage3;

    private void Awake()
    {
        stage1 = transform.GetChild(0).GetComponent<Button>();
        stage2 = transform.GetChild(1).GetComponent<Button>();
        stage3 = transform.GetChild(2).GetComponent<Button>();
    }

    private void Start()
    {
        stage1.onClick.AddListener(() => SceneChanger.Instance.SelectStage(1));
    }
}
