using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    private Image hpBar;
    [SerializeField] private TextMeshProUGUI hpText;
    private void Awake()
    {
        hpBar = GetComponent<Image>();
    }
    private void Start()
    {
        UpdateHPBar();
        PlayerStatManager.Instance.OnChangeHp += UpdateHPBar;
    }

    private void UpdateHPBar()
    {
        hpBar.fillAmount = PlayerStatManager.Instance.Hp * 10;
        hpText.text = $"{PlayerStatManager.Instance.Hp} /\n {PlayerStatManager.Instance.MaxHp}";
    }
}
