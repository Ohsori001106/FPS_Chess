using UnityEngine;
using UnityEngine.UI;

public class RenderLine : MonoBehaviour
{
    bool dragging = false; // 드래그 상태를 추적하는 변수
    LineRenderer lineRenderer; // 선을 그리기 위한 LineRenderer 컴포넌트의 참조

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red; // 선의 시작 색상
        lineRenderer.endColor = Color.yellow; // 선의 끝 색상
        lineRenderer.startWidth = 0.5f; // 선의 시작 너비
        lineRenderer.endWidth = 0.5f; // 선의 끝 너비
        lineRenderer.positionCount = 2; // 선을 구성할 점의 수
        lineRenderer.useWorldSpace = true; // 월드 좌표계 사용 설정
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)); // Z-축은 오브젝트와 카메라 거리 고려
            mousePosition.z = transform.position.z; // Z-축 설정

            Vector3 direction = mousePosition - transform.position; // 방향 벡터 계산
            Vector3 normalizedDirection = direction.normalized; // 정규화된 방향 벡터

            lineRenderer.SetPosition(0, transform.position - normalizedDirection * 2); // 방향의 반대쪽으로 선을 확장
            lineRenderer.SetPosition(1, transform.position + normalizedDirection * 2); // 마우스 방향으로 선을 확장
        }
    }

    void OnMouseDown()
    {
        dragging = true;
        lineRenderer.enabled = true; // 라인 렌더러 활성화
    }

    void OnMouseUp()
    {
        dragging = false;
        lineRenderer.enabled = false; // 드래그가 끝나면 라인 렌더러 비활성화
    }
}
