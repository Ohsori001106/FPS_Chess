using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("�濡 ���������� �����Ͽ����ϴ�.");
        // �濡 �����ϸ� �ʿ��� �ʱ�ȭ�� ������ ������ �� �ֽ��ϴ�.
    }

    // ���� ������ �ʿ��� �߰����� �޼ҵ���� �̰��� ������ �� �ֽ��ϴ�.
    // ���� ���, ���� ���� ���� Ȯ��, ���� ���, ���� ����� ��
}
