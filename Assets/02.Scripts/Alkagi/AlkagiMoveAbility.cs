using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkagiMoveAbility : MonoBehaviour
{
    private Rigidbody rb; // �� ������Ʈ�� Rigidbody ������Ʈ
    public float maxPower = 10f; // �߻��� �� ������ �ִ� ��
    private Vector3 startPosition; // �巡�� ���� ��ġ
    private Vector3 endPosition; // �巡�� �� ��ġ
    private bool isDragging = false; // �巡�� ������ ���¸� ��Ÿ���� �÷���
    private Camera cam; // ī�޶� ĳ��

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� ������
        cam = Camera.main; // ���� ī�޶� ��������
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ó�� ���� ��
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f)) // ���콺 ��ġ���� ����ĳ��Ʈ�� �߻�
            {
                if (hit.collider.gameObject == gameObject) // Ŭ���� ������Ʈ�� �� ���� ������Ʈ���� Ȯ��
                {
                    startPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
                    isDragging = true; // �巡�� ����
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // ���콺 ���� ��ư�� �������� �巡�� ���̾�����
        {
            endPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
            isDragging = false; // �巡�� ����
            Shoot(); // �߻�
        }
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition; // ���� ���� ���
        float dragDistance = Vector3.Distance(startPosition, endPosition); // �巡�� �Ÿ� ���
        float appliedPower = Mathf.Min(dragDistance * 2, maxPower); // �巡�� �Ÿ��� ���� �Ŀ� ���� (�ִ� �Ŀ� ����)

        rb.AddForce(forceDirection.normalized * appliedPower, ForceMode.Impulse); // Rigidbody�� ���� ����
    }
}
