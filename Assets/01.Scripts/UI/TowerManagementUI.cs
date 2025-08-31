using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class TowerManagementUI : MonoBehaviour
{
    public TowerBase TowerBase;
    public Button BuyTowerButton;
    public Button UpgrageTowerButton;
    public Button HideButton;
    private TextMeshProUGUI hideButtonText;

    RectTransform rectTransform;
    private bool isHide;
    private Vector2 unhidePos;
    private Vector2 hidePos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        unhidePos = rectTransform.anchoredPosition;
        hidePos = new Vector2(460, rectTransform.anchoredPosition.y);
        hideButtonText = HideButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void BuyTower()
    {
        Debug.Log($"{TowerSpawnManager.Instance.TowerPrice} Å×½ºÆ®2 ");
        if (TowerBase != null &&TowerBase.isSpawned == false && PlayerStatManager.Instance.Energy >= TowerSpawnManager.Instance.TowerPrice)
        {
            GameObject towerObj = Instantiate(TowerSpawnManager.Instance.Spawn(), TowerBase.transform.GetChild(0).position, Quaternion.identity);
            PlayerStatManager.Instance.ConsumeEnergyToSpawnTower();
            towerObj.transform.parent = TowerBase.transform.GetChild(0);
            TowerBase.isSpawned = true;
        }
    }

    public void UpgradeTower()
    {
        if(TowerBase.isSpawned == true && PlayerStatManager.Instance.Energy >= TowerSpawnManager.Instance.TowerPrice)
        {
            PlayerStatManager.Instance.ConsumeEnergyToSpawnTower();
            Tower tower = TowerBase.transform.GetChild(0).GetChild(0).GetComponent<Tower>();
            TowerSpawnManager.Instance.UpgradeTower(tower);
        }
    }

    public void HideUI()
    {
        if(isHide == false) 
        {
            StartCoroutine(HideRoutine());
        }
        else
        {
            StartCoroutine(UnhideRoutine());
        }
    }

    IEnumerator HideRoutine()
    {
        while(true) 
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, hidePos, Time.deltaTime * 7f);
            if(Vector3.Distance(rectTransform.anchoredPosition, hidePos) <= 0.5f)
            {
                isHide = true;
                hideButtonText.text = "¢¸";
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator UnhideRoutine()
    {
        while(true)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, unhidePos, Time.deltaTime * 7f);
            if (Vector3.Distance(rectTransform.anchoredPosition, unhidePos) <= 0.5f)
            {
                isHide = false;
                hideButtonText.text = "¢º";
                yield break;
            }
            yield return null;
        }
    }
}
