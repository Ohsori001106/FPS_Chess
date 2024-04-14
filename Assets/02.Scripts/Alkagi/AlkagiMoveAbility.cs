using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkagiMoveAbility : MonoBehaviour
{
    private Rigidbody rb; // 이 오브젝트의 Rigidbody 컴포넌트
    public float maxPower = 10f; // 발사할 때 적용할 최대 힘
    private Vector3 startPosition; // 드래그 시작 위치
    private Vector3 endPosition; // 드래그 끝 위치
    private bool isDragging = false; // 드래그 중인지 상태를 나타내는 플래그
    private Camera cam; // 카메라 캐시

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옴
        cam = Camera.main; // 메인 카메라 가져오기
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼이 처음 눌릴 때
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f)) // 마우스 위치에서 레이캐스트를 발사
            {
                if (hit.collider.gameObject == gameObject) // 클릭된 오브젝트가 이 게임 오브젝트인지 확인
                {
                    startPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
                    isDragging = true; // 드래그 시작
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // 마우스 왼쪽 버튼이 떼어지고 드래그 중이었으면
        {
            endPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
            isDragging = false; // 드래그 종료
            Shoot(); // 발사
        }
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition; // 힘의 방향 계산
        float dragDistance = Vector3.Distance(startPosition, endPosition); // 드래그 거리 계산
        float appliedPower = Mathf.Min(dragDistance * 2, maxPower); // 드래그 거리에 따라 파워 결정 (최대 파워 제한)

        rb.AddForce(forceDirection.normalized * appliedPower, ForceMode.Impulse); // Rigidbody에 힘을 가함
    }
}
