using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        name = "����Ÿ��";
        attackDelay = 1;
    }
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO : ���Ͱ� Ʈ���ſ� ������ ���� �ڷ�ƾ ȣ��
        if (other.gameObject.GetComponent<Test>())
        {
            Debug.Log("��������");
            Debug.Log($"����{isAttack}");

            Attack(this);
        }
    }
}
