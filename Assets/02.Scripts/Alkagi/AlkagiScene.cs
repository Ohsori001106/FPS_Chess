using System.Collections;
using UnityEngine;
using Photon.Pun; // Photon ��Ʈ��ũ ����� ����ϱ� ���� �߰�

public class AlkagiScene : MonoBehaviourPunCallbacks // PhotonPunCallbacks�� ��ӹ���
{
    void Start()
    {
        // ��� ��Ʈ��ũ ���� �۾��� �濡 ���������� ������ �Ŀ� ����Ǿ�� �մϴ�.
        // �̸� ���� OnJoinedRoom() ������ ���� ������ ó���ϵ��� ����
    }

    public override void OnJoinedRoom()
    {
        // �濡 �������� �� Al �������� ����
        PhotonNetwork.Instantiate("Al", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
