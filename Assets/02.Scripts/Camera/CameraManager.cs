using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviourPunCallbacks
{
    public Camera[] cameras; // �ν����Ϳ��� �Ҵ��� ī�޶� �迭

    void Start()
    {
        // ��� ī�޶� �ʱ⿡ ��Ȱ��ȭ
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        AssignCameraToPlayers();
    }

    void AssignCameraToPlayers()
    {
        Debug.Log($"Total players: {PhotonNetwork.PlayerList.Length}, Cameras: {cameras.Length}");
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            int cameraIndex = (PhotonNetwork.PlayerList[i].ActorNumber - 1) % cameras.Length;
            Debug.Log($"Assigning Camera {cameraIndex} to Player {PhotonNetwork.PlayerList[i].NickName} ({PhotonNetwork.PlayerList[i].ActorNumber})");

            if (i < cameras.Length)
            {
                if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                {
                    cameras[cameraIndex].gameObject.SetActive(true);
                    Debug.Log($"Camera {cameraIndex} activated for {PhotonNetwork.LocalPlayer.NickName}");
                }
            }
            else
            {
                Debug.LogError("Not enough cameras for all players.");
            }
        }
    }
}
