using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        name = "마법타워";
        attackDelay = 1;
    }
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO : 몬스터가 트리거에 있으면 공격 코루틴 호출
        if (other.gameObject.GetComponent<Test>())
        {
            Debug.Log("마법접촉");
            Debug.Log($"마법{isAttack}");

            Attack(this);
        }
    }
}
