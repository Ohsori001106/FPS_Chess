using UnityEngine;
using UnityEngine.UI;

public class RenderLine : MonoBehaviour
{
    bool dragging = false; // �巡�� ���¸� �����ϴ� ����
    LineRenderer lineRenderer; // ���� �׸��� ���� LineRenderer ������Ʈ�� ����

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red; // ���� ���� ����
        lineRenderer.endColor = Color.yellow; // ���� �� ����
        lineRenderer.startWidth = 0.5f; // ���� ���� �ʺ�
        lineRenderer.endWidth = 0.5f; // ���� �� �ʺ�
        lineRenderer.positionCount = 2; // ���� ������ ���� ��
        lineRenderer.useWorldSpace = true; // ���� ��ǥ�� ��� ����
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)); // Z-���� ������Ʈ�� ī�޶� �Ÿ� ���
            mousePosition.z = transform.position.z; // Z-�� ����

            Vector3 direction = mousePosition - transform.position; // ���� ���� ���
            Vector3 normalizedDirection = direction.normalized; // ����ȭ�� ���� ����

            lineRenderer.SetPosition(0, transform.position - normalizedDirection * 2); // ������ �ݴ������� ���� Ȯ��
            lineRenderer.SetPosition(1, transform.position + normalizedDirection * 2); // ���콺 �������� ���� Ȯ��
        }
    }

    void OnMouseDown()
    {
        dragging = true;
        lineRenderer.enabled = true; // ���� ������ Ȱ��ȭ
    }

    void OnMouseUp()
    {
        dragging = false;
        lineRenderer.enabled = false; // �巡�װ� ������ ���� ������ ��Ȱ��ȭ
    }
}
