using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    private Image hpBar;
    private TextMeshProUGUI hpText;
    private void Awake()
    {
        hpBar = GetComponent<Image>();
        hpText = hpBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpdateHPBar();
        PlayerStatManager.Instance.OnChangeHp += UpdateHPBar;
    }

    private void UpdateHPBar()
    {
        hpBar.fillAmount = PlayerStatManager.Instance.Hp * 10;
        hpText.text = $"{PlayerStatManager.Instance.Hp} / {PlayerStatManager.Instance.MaxHp}";
    }
}
