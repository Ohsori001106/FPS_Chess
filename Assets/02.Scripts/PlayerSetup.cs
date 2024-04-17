using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject[] cameras; // 인스펙터에서 할당

    void Start()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;

        // 모든 카메라를 비활성화
        foreach (var camera in cameras)
        {
            camera.SetActive(false);
        }

        // 현재 플레이어의 카메라만 활성화
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
