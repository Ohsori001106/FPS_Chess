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

  
    // ���� ������ �ʿ��� �߰����� �޼ҵ���� �̰��� ������ �� �ֽ��ϴ�.
    // ���� ���, ���� ���� ���� Ȯ��, ���� ���, ���� ����� ��
}
