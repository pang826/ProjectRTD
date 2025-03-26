using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManagementUI : MonoBehaviour
{
    public TowerBase TowerBase;
    public Button BuyTowerButton;
    public Button UpgrageTowerButton;

    public void BuyTower()
    {
        if(TowerBase.isSpawned == false)
        {
            GameObject towerObj = Instantiate(TowerSpawnManager.Instance.Spawn(), TowerBase.transform.position, Quaternion.identity);
            towerObj.transform.parent = TowerBase.transform.GetChild(0);
            TowerBase.isSpawned = true;
        }
    }

    public void UpgradeTower()
    {
        if(TowerBase.isSpawned == true)
        {

        }
    }
}
