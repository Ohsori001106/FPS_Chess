using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAttackAbility : ChessAbility
{
    public ParticleSystem hitEffectPrefab; // 충돌 이펙트 프리팹
    public Transform firePoint; // 총알 발사 위치
    public float fireRate = 1f; // 총알 발사 간격
    private float nextFireTime = 0f; // 다음 발사 시간

    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하고, 발사 간격을 초과한 경우
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            // 총알 발사 함수 호출
            Shoot();
            // 발사 간격 설정
            nextFireTime = Time.time + fireRate;
        }
    }

    // 총알 발사 함수
    void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            ParticleSystem hitEffect = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Play();

            // "White" 태그를 가진 체스 말에 데미지 적용
            if (hitInfo.collider.CompareTag("White"))
            {
                Stat targetStat = hitInfo.collider.GetComponent<Stat>();
                if (targetStat != null)
                {
                    // 상대방 체스 말의 체력 감소
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
