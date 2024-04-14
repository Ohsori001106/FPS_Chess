using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FPSCamera : MonoBehaviour
{
    public float RotationSpeed = 200; // ī�޶� ȸ�� �ӵ�
    private float _mx = 0;
    private float _my = 0;
    public Transform Target;  // ī�޶� ���� ���

    private void Start()
    {
        // Ŀ�� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // �÷��̾��� �� ����
        AssignTeamAndSetCameraTarget();
    }

    // ���� �Ҵ��ϰ� ī�޶� Ÿ���� �����ϴ� �޼���
    private void AssignTeamAndSetCameraTarget()
    {
        string team = PhotonNetwork.LocalPlayer.CustomProperties["Team"] as string;
        if (string.IsNullOrEmpty(team))
        {
            // �� ������ ������ �������� ���� �Ҵ�
            team = Random.Range(0, 2) == 0 ? "Black" : "White";
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }

        SetCameraTarget(team);
    }

    // ������ �� �±׸� ������� Ÿ���� �����ϴ� �޼���
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
            // �±װ� ���� ��� �����տ��� �� ������Ʈ ����
            GameObject prefab = Resources.Load<GameObject>("Prefabs/TeamMember"); // ���� ������ ���
            if (prefab != null)
            {
                GameObject newMember = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                newMember.tag = team;  // ������ ������Ʈ�� �±� �Ҵ�
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
            // Ÿ�� ��ġ�� ī�޶� �̵�
            transform.position = Target.position;
        }

        // ���콺 �Է¿� ���� ī�޶� ȸ��
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _mx += mouseX * RotationSpeed * Time.deltaTime;
        _my += mouseY * RotationSpeed * Time.deltaTime;

        _my = Mathf.Clamp(_my, -90f, 90f); // ���� ȸ�� ����

        transform.eulerAngles = new Vector3(-_my, _mx, 0);
    }
}
