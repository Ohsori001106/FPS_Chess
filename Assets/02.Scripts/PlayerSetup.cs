using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject[] cameras; // �ν����Ϳ��� �Ҵ�

    void Start()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;

        // ��� ī�޶� ��Ȱ��ȭ
        foreach (var camera in cameras)
        {
            camera.SetActive(false);
        }

        // ���� �÷��̾��� ī�޶� Ȱ��ȭ
        if (cameras.Length > playerIndex && cameras[playerIndex] != null)
        {
            cameras[playerIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Assigned camera not found or index out of range!");
        }
    }
}
