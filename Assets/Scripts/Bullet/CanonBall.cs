using System.Collections;
using UnityEngine;

public class CanonBall : Bullet
{
    private void Start()
    {
        poolType = E_PoolType.MagicBall;
        damage = 5;
    }
    private void OnEnable()
    {
        StartCoroutine(DeleteRoutine());
    }

    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5);
        transform.LookAt(targetPos);
    }
    IEnumerator DeleteRoutine()
    {
        targetPos = Vector3.zero;
        yield return new WaitForSeconds(3);
        
        ObjectPoolManager.Instance.ReturnObject(this.poolType, gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            // ���� ���� ����
            float explosionRadius = 2.0f;
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius,1 << 3);

            foreach (var hit in hits)
            {
                IDamageable target = hit.gameObject.GetComponent<IDamageable>();
                if (target != null)
                {
                    Debug.Log("������");
                    target.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("��");
                }
            }

            // ���� ����Ʈ �� �߰� ����
            // Instantiate(explosionEffect, transform.position, Quaternion.identity);

            ObjectPoolManager.Instance.ReturnObject(this.poolType, gameObject);
        }
    }
}
