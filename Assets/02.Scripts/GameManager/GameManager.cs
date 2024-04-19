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

  
    // 게임 로직이 필요한 추가적인 메소드들을 이곳에 구현할 수 있습니다.
    // 예를 들어, 게임 오버 조건 확인, 점수 계산, 게임 재시작 등
}
