using UnityEngine;
using Photon.Pun;  // Photon 네트워크를 사용하기 위해 추가

public class RenderLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    bool dragging = false;
    public float maxLength = 10f; // 선의 최대 길이 설정
    PhotonView photonView;  // PhotonView 컴포넌트를 저장할 변수

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 4;
        lineRenderer.useWorldSpace = true;

        photonView = GetComponent<PhotonView>();  // PhotonView 컴포넌트 가져오기
    }

    void Update()
    {
        if (dragging && photonView.IsMine)  // 본인의 코인일 때만 선을 그릴 수 있도록 검사
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
            mousePosition.y = transform.position.y; // Y축 고정

            Vector3 direction = mousePosition - transform.position; // 방향 벡터 계산
            direction.y = 0; // Y 축 변화 무시

            // 방향을 정규화하고 최대 길이로 제한
            Vector3 lineEndPosition = transform.position + direction.normalized * Mathf.Min(direction.magnitude, maxLength);
            Vector3 oppositeLineEndPosition = transform.position - direction.normalized * Mathf.Min(direction.magnitude, maxLength);

            lineRenderer.SetPosition(0, oppositeLineEndPosition); // 반대 방향 끝점
            lineRenderer.SetPosition(1, transform.position); // 중앙점
            lineRenderer.SetPosition(2, transform.position); // 중앙점
            lineRenderer.SetPosition(3, lineEndPosition); // 마우스 방향 끝점
        }
    }

    void OnMouseDown()
    {
        if (photonView.IsMine)  // 본인의 코인일 때만 드래깅 시작
        {
            dragging = true;
            lineRenderer.enabled = true;
        }
    }

    void OnMouseUp()
    {
        if (photonView.IsMine)
        {
            dragging = false;
            lineRenderer.enabled = false;
        }
    }
}
