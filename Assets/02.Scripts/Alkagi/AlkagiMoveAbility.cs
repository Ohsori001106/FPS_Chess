using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkagiMoveAbility : MonoBehaviour
{
    public static List<AlkagiMoveAbility> AllShooters = new List<AlkagiMoveAbility>(); // ��� AlkagiMoveAbility �ν��Ͻ��� �����ϴ� ���� ����Ʈ

    private Rigidbody rb; // �� ������Ʈ�� Rigidbody ������Ʈ
    public float maxPower = 10f; // �߻��� �� ������ �ִ� ��
    private Vector3 startPosition; // �巡�� ���� ��ġ
    private Vector3 endPosition; // �巡�� �� ��ġ
    private bool isDragging = false; // �巡�� ������ ���¸� ��Ÿ���� �÷���

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� ������
        AllShooters.Add(this); // ���� �ν��Ͻ��� ��ü ���� ����Ʈ�� �߰�
    }

    void OnDestroy()
    {
        AllShooters.Remove(this); // ���� �ν��Ͻ��� ��ü ���� ����Ʈ���� ����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ó�� ���� ��
        {
            AlkagiMoveAbility closest = FindClosestToMouse(); // ���콺 �����̿� �ִ� ������Ʈ ã��
            if (closest == this) // ���� ����� ������Ʈ�� �ڱ� �ڽ��̸�
            {
                startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
                isDragging = true; // �巡�� ����
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // ���콺 ���� ��ư�� �������� �巡�� ���̾�����
        {
            endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            isDragging = false; // �巡�� ����
            Shoot(); // �߻�
        }
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition; // ���� ���� ���
        float dragDistance = Vector3.Distance(startPosition, endPosition); // �巡�� �Ÿ� ���
        float appliedPower = Mathf.Min(dragDistance, maxPower); // �巡�� �Ÿ��� ���� �Ŀ� ���� (�ִ� �Ŀ� ����)

        rb.AddForce(forceDirection.normalized * appliedPower, ForceMode.Impulse); // Rigidbody�� ���� ����
    }

    public static AlkagiMoveAbility FindClosestToMouse()
    {
        float closestDistance = float.MaxValue; // ���� ����� �Ÿ� �ʱ�ȭ
        AlkagiMoveAbility closest = null; // ���� ����� ������Ʈ �ʱ�ȭ
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y)); // ���콺 ��ġ�� 3D ���� ��ǥ�� ��ȯ

        foreach (var shooter in AllShooters) // ��� ���͸� �ݺ�
        {
            float distance = Vector3.Distance(mousePos, shooter.transform.position); // ���콺 ��ġ���� �Ÿ� ���
            if (distance < closestDistance) // �� �Ÿ��� ���ݱ��� ã�� ���� ����� �Ÿ����� ª����
            {
                closestDistance = distance; // �ּ� �Ÿ� ����
                closest = shooter; // ���� ����� ���� ����
            }
        }
        return closest; // ���� ����� ���� ��ȯ
    }
}
