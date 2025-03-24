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
        Instantiate(TowerSpawnManager.Instance.Spawn(), TowerBase.transform.position, Quaternion.identity);
    }
}
