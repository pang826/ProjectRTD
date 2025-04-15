using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MPUI : MonoBehaviour
{
    private Image mpBar;
    private TextMeshProUGUI mpText;
    private void Awake()
    {
        mpBar = GetComponent<Image>();
        mpText = mpBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        PlayerStatManager.Instance.OnChangeMP += UpdateMpBar;
        PlayerStatManager.Instance.OnChangeMP?.Invoke();
    }

    private void UpdateMpBar()
    {
        mpBar.fillAmount = PlayerStatManager.Instance.Mp * 0.05f;
        mpText.text = $"{PlayerStatManager.Instance.Mp} / {PlayerStatManager.Instance.MaxMp}";
    }
}
