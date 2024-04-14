using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAttackAbility : ChessAbility
{
    public ParticleSystem hitEffectPrefab; // �浹 ����Ʈ ������
    public Transform firePoint; // �Ѿ� �߻� ��ġ
    public float fireRate = 1f; // �Ѿ� �߻� ����
    private float nextFireTime = 0f; // ���� �߻� �ð�

    private void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϰ�, �߻� ������ �ʰ��� ���
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            // �Ѿ� �߻� �Լ� ȣ��
            Shoot();
            // �߻� ���� ����
            nextFireTime = Time.time + fireRate;
        }
    }

    // �Ѿ� �߻� �Լ�
    void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            ParticleSystem hitEffect = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Play();

            // "White" �±׸� ���� ü�� ���� ������ ����
            if (hitInfo.collider.CompareTag("White"))
            {
                Stat targetStat = hitInfo.collider.GetComponent<Stat>();
                if (targetStat != null)
                {
                    // ���� ü�� ���� ü�� ����
                    targetStat.Health -= _owner.Stat.Damage;
                    Debug.Log("Damage dealt: " + _owner.Stat.Damage + ", Target Health: " + targetStat.Health);
                }
            }
        }
        else
        {
            ParticleSystem hitEffect = Instantiate(hitEffectPrefab, firePoint.position + firePoint.forward * 100f, Quaternion.identity);
            hitEffect.transform.forward = firePoint.forward;
            hitEffect.Play();
        }
    }
}
