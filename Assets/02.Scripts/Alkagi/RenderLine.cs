using UnityEngine;
using Photon.Pun;  // Photon ��Ʈ��ũ�� ����ϱ� ���� �߰�

public class RenderLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    bool dragging = false;
    public float maxLength = 10f; // ���� �ִ� ���� ����
    PhotonView photonView;  // PhotonView ������Ʈ�� ������ ����

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

        photonView = GetComponent<PhotonView>();  // PhotonView ������Ʈ ��������
    }

    void Update()
    {
        if (dragging && photonView.IsMine)  // ������ ������ ���� ���� �׸� �� �ֵ��� �˻�
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
            mousePosition.y = transform.position.y; // Y�� ����

            Vector3 direction = mousePosition - transform.position; // ���� ���� ���
            direction.y = 0; // Y �� ��ȭ ����

            // ������ ����ȭ�ϰ� �ִ� ���̷� ����
            Vector3 lineEndPosition = transform.position + direction.normalized * Mathf.Min(direction.magnitude, maxLength);
            Vector3 oppositeLineEndPosition = transform.position - direction.normalized * Mathf.Min(direction.magnitude, maxLength);

            lineRenderer.SetPosition(0, oppositeLineEndPosition); // �ݴ� ���� ����
            lineRenderer.SetPosition(1, transform.position); // �߾���
            lineRenderer.SetPosition(2, transform.position); // �߾���
            lineRenderer.SetPosition(3, lineEndPosition); // ���콺 ���� ����
        }
    }

    void OnMouseDown()
    {
        if (photonView.IsMine)  // ������ ������ ���� �巡�� ����
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
