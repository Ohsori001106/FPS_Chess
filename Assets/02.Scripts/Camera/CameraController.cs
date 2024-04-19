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
        if (scene.name == "MainScene") // MainScene�� �ε���� ���� ����
        {
            SetCameraRotation();
        }
    }

    void SetCameraRotation()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            // �÷��̾� �ε����� ��������, 1���� �����ϹǷ� 1�� ���ݴϴ�.
            int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            float zRotation = GetCameraRotationForPlayer(playerIndex);

            // ī�޶��� ȸ���� �����մϴ�. X ȸ���� 90���� �����մϴ�.
            Camera.main.transform.rotation = Quaternion.Euler(90, 0, zRotation);
        }
    }

    float GetCameraRotationForPlayer(int index)
    {
        // �÷��̾��� �ε����� ���� ȸ������ ����
        switch (index % 4) // 4���� �÷��̾ �ִٰ� ����
        {
            case 0: return 0;    // ù ��° �÷��̾�
            case 1: return -180; // �� ��° �÷��̾�
            case 2: return -90;  // �� ��° �÷��̾�
            case 3: return 90;   // �� ��° �÷��̾�
            default: return 0;   // �⺻��
        }
    }
}
