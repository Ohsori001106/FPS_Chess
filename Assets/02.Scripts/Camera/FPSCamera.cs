using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FPSCamera : MonoBehaviour
{
    public float RotationSpeed = 200; // 카메라 회전 속도
    private float _mx = 0;
    private float _my = 0;
    public Transform Target;  // 카메라가 따라갈 대상

    private void Start()
    {
        // 커서 설정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 플레이어의 팀 설정
        AssignTeamAndSetCameraTarget();
    }

    // 팀을 할당하고 카메라 타겟을 설정하는 메서드
    private void AssignTeamAndSetCameraTarget()
    {
        string team = PhotonNetwork.LocalPlayer.CustomProperties["Team"] as string;
        if (string.IsNullOrEmpty(team))
        {
            // 팀 정보가 없으면 랜덤으로 팀을 할당
            team = Random.Range(0, 2) == 0 ? "Black" : "White";
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }

        SetCameraTarget(team);
    }

    // 지정된 팀 태그를 기반으로 타겟을 설정하는 메서드
    private void SetCameraTarget(string team)
    {
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(team);
        if (potentialTargets.Length > 0)
        {
            Target = potentialTargets[Random.Range(0, potentialTargets.Length)].transform;
        }
        else
        {
            Debug.LogError("No objects with tag " + team + " found.");
            // 태그가 없는 경우 프리팹에서 새 오브젝트 생성
            GameObject prefab = Resources.Load<GameObject>("Prefabs/TeamMember"); // 예시 프리팹 경로
            if (prefab != null)
            {
                GameObject newMember = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                newMember.tag = team;  // 생성된 오브젝트에 태그 할당
                Target = newMember.transform;
                Debug.Log(team + " team member created and assigned as target.");
            }
            else
            {
                Debug.LogError("Failed to load team member prefab.");
            }
        }
    }

    private void LateUpdate()
    {
        if (Target != null)
        {
            // 타겟 위치로 카메라 이동
            transform.position = Target.position;
        }

        // 마우스 입력에 따른 카메라 회전
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _mx += mouseX * RotationSpeed * Time.deltaTime;
        _my += mouseY * RotationSpeed * Time.deltaTime;

        _my = Mathf.Clamp(_my, -90f, 90f); // 상하 회전 제한

        transform.eulerAngles = new Vector3(-_my, _mx, 0);
    }
}
