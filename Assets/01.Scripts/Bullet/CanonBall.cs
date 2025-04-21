using System.Collections;
using UnityEngine;

public class CanonBall : Bullet
{
    [SerializeField] private ParticleSystem particle;
    private void Start()
    {
        poolType = E_PoolType.CanonBall;
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
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
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
        if (other.gameObject.layer == 3 || other.gameObject.layer == 9)
        {
            ObjectPoolManager.Instance.GetObject(E_PoolType.CanonBallEffect, transform);
            // 폭발 범위 판정
            float explosionRadius = 2.0f;
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 3);

            foreach (var hit in hits)
            {
                IDamageable target = hit.gameObject.GetComponent<IDamageable>();
                if (target != null)
                {
                    Debug.Log("데미지");
                    target.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("널");
                }

            }

            // 폭발 이펙트 등 추가 가능
            // Instantiate(explosionEffect, transform.position, Quaternion.identity);

            ObjectPoolManager.Instance.ReturnObject(this.poolType, gameObject);
        }

    }
}
