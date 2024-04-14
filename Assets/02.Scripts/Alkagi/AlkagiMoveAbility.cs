using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkagiMoveAbility : MonoBehaviour
{
    public static List<AlkagiMoveAbility> AllShooters = new List<AlkagiMoveAbility>(); // 모든 AlkagiMoveAbility 인스턴스를 추적하는 정적 리스트

    private Rigidbody rb; // 이 오브젝트의 Rigidbody 컴포넌트
    public float maxPower = 10f; // 발사할 때 적용할 최대 힘
    private Vector3 startPosition; // 드래그 시작 위치
    private Vector3 endPosition; // 드래그 끝 위치
    private bool isDragging = false; // 드래그 중인지 상태를 나타내는 플래그

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옴
        AllShooters.Add(this); // 현재 인스턴스를 전체 슈터 리스트에 추가
    }

    void OnDestroy()
    {
        AllShooters.Remove(this); // 현재 인스턴스를 전체 슈터 리스트에서 제거
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼이 처음 눌릴 때
        {
            AlkagiMoveAbility closest = FindClosestToMouse(); // 마우스 가까이에 있는 오브젝트 찾기
            if (closest == this) // 가장 가까운 오브젝트가 자기 자신이면
            {
                startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
                isDragging = true; // 드래그 시작
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // 마우스 왼쪽 버튼이 떼어지고 드래그 중이었으면
        {
            endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            isDragging = false; // 드래그 종료
            Shoot(); // 발사
        }
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition; // 힘의 방향 계산
        float dragDistance = Vector3.Distance(startPosition, endPosition); // 드래그 거리 계산
        float appliedPower = Mathf.Min(dragDistance, maxPower); // 드래그 거리에 따라 파워 결정 (최대 파워 제한)

        rb.AddForce(forceDirection.normalized * appliedPower, ForceMode.Impulse); // Rigidbody에 힘을 가함
    }

    public static AlkagiMoveAbility FindClosestToMouse()
    {
        float closestDistance = float.MaxValue; // 가장 가까운 거리 초기화
        AlkagiMoveAbility closest = null; // 가장 가까운 오브젝트 초기화
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y)); // 마우스 위치를 3D 공간 좌표로 변환

        foreach (var shooter in AllShooters) // 모든 슈터를 반복
        {
            float distance = Vector3.Distance(mousePos, shooter.transform.position); // 마우스 위치와의 거리 계산
            if (distance < closestDistance) // 이 거리가 지금까지 찾은 가장 가까운 거리보다 짧으면
            {
                closestDistance = distance; // 최소 거리 갱신
                closest = shooter; // 가장 가까운 슈터 갱신
            }
        }
        return closest; // 가장 가까운 슈터 반환
    }
}
