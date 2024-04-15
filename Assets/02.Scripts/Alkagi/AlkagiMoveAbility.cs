using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkagiMoveAbility : MonoBehaviour
{
    private Rigidbody rb;
    public float maxPower = 10f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isDragging = false;
    private Camera cam;

    // �浹 �� �ݹ߷��� ����
    public float bounceForce = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    startPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
            isDragging = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition;
        float dragDistance = Vector3.Distance(startPosition, endPosition);
        float appliedPower = Mathf.Min(dragDistance * 3, maxPower);

        rb.AddForce(forceDirection.normalized * appliedPower, ForceMode.Impulse);
    }

    // �浹 ó�� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) // Player �±׿͸� �ݹ߷� ����
        {
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
