using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    private Image energyBar;
    [SerializeField] private TextMeshProUGUI energyText;
    private void Awake()
    {
        energyBar = GetComponent<Image>();
    }
    private void Start()
    {
        PlayerStatManager.Instance.OnChangeEnergy += UpdateEnergyBar;
    }

    private void UpdateEnergyBar()
    {
        energyBar.fillAmount = PlayerStatManager.Instance.Energy * 0.01f;
        energyText.text = $"{PlayerStatManager.Instance.Energy} /\n {PlayerStatManager.Instance.MaxEnergy}";
    }
}
