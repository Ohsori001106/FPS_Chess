using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    void Awake()
    {
        // 이 게임 오브젝트를 새 씬 로드 시에 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 초기화 코드는 여기에 작성
    }

    // 체스말의 기타 함수들을 여기에 추가할 수 있습니다
}