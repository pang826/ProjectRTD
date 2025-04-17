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

    private bool isHide;
    private Vector3 unhidePos;
    private Vector3 hidePos;

    private void Awake()
    {
        unhidePos = transform.position;
        hidePos = transform.position + new Vector3(458, 0, 0);
        hideButtonText = HideButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void BuyTower()
    {
        if(TowerBase != null &&TowerBase.isSpawned == false && PlayerStatManager.Instance.Energy >= TowerSpawnManager.Instance.TowerPrice)
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
            tower.UpgradeTower(tower.Lv2Dmg, tower.Lv3Dmg);
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
            transform.position = Vector3.Lerp(transform.position, hidePos, Time.deltaTime * 7f);
            if(Vector3.Distance(transform.position, hidePos) <= 0.5f)
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
            transform.position = Vector3.Lerp(transform.position, unhidePos, Time.deltaTime * 7f);
            if(Vector3.Distance(transform.position, unhidePos) <= 0.5f)
            {
                isHide = false;
                hideButtonText.text = "¢º";
                yield break;
            }
            yield return null;
        }
    }
}
