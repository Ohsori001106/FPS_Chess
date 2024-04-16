using System.Collections;
using UnityEngine;
using Photon.Pun;

public class AlkagiScene : MonoBehaviourPunCallbacks
{
    public Transform blackPawnPosition; // Black ���� Pawn�� ������ ��ġ
    public Transform whitePawnPosition; // White ���� Pawn�� ������ ��ġ

    public override void OnJoinedRoom()
    {
        // �濡 �������� �� ������ ���� ���� ������ �����հ� ��ġ���� ����
        string team = PhotonNetwork.LocalPlayer.CustomProperties["Team"] as string;

        if (team == "Black")
        {
            PhotonNetwork.Instantiate("Pawn Black", blackPawnPosition.position, Quaternion.identity);
        }
        else if (team == "White")
        {
            PhotonNetwork.Instantiate("Pawn White", whitePawnPosition.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("�� ������ �ùٸ��� �ʽ��ϴ�.");
        }
    }
}
