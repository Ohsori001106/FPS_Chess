using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    void Awake()
    {
        // �� ���� ������Ʈ�� �� �� �ε� �ÿ� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // �ʱ�ȭ �ڵ�� ���⿡ �ۼ�
    }

    // ü������ ��Ÿ �Լ����� ���⿡ �߰��� �� �ֽ��ϴ�
}