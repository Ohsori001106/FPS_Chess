using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class ChessRotateAbility : ChessAbility
{
    public Transform CameraRoot;
   

    private float _mx;
    private float _my;

    private void Start()
    {
        /*if (_owner.PhotonView.IsMine)
        {
            
            GameObject.FindWithTag("FollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = CameraRoot;
        }*/
    }

    private void Update()
    {
        
        // ����
        // 1. ���콺 �Է� ���� �޴´�
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 2. ȸ�� ���� ���콺 �Է¿� ���� �̸� �����Ѵ�
        _mx += mouseX * _owner.Stat.RotationSpeed * Time.deltaTime;
        _my += mouseY * _owner.Stat.RotationSpeed * Time.deltaTime;
        _my = Mathf.Clamp(_my, -90f, 90f);

        // 3. ī�޶�(TPS)�� ĳ���͸� ȸ�� �������� ȸ����Ų��
        transform.eulerAngles = new Vector3(0, _mx, 0f);
        CameraRoot.localEulerAngles = new Vector3(-_my, 0, 0f);
    }

    // �����ϰ� ������ �� ������ �����ϰ�
    public void SetRandomRotation()
    {
        _mx = UnityEngine.Random.Range(0, 360);
        _my = 0;
    }
}
