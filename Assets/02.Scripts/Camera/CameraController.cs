using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviourPun
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene") // MainScene이 로드됐을 때만 실행
        {
            SetCameraRotation();
        }
    }

    void SetCameraRotation()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            // 플레이어 인덱스를 가져오고, 1부터 시작하므로 1을 빼줍니다.
            int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            float zRotation = GetCameraRotationForPlayer(playerIndex);

            // 카메라의 회전을 설정합니다. X 회전은 90도로 고정합니다.
            Camera.main.transform.rotation = Quaternion.Euler(90, 0, zRotation);
        }
    }

    float GetCameraRotationForPlayer(int index)
    {
        // 플레이어의 인덱스에 따라 회전값을 설정
        switch (index % 4) // 4명의 플레이어가 있다고 가정
        {
            case 0: return 0;    // 첫 번째 플레이어
            case 1: return -180; // 두 번째 플레이어
            case 2: return -90;  // 세 번째 플레이어
            case 3: return 90;   // 네 번째 플레이어
            default: return 0;   // 기본값
        }
    }
}
