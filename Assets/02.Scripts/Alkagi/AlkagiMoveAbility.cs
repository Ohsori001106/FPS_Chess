using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AlkagiMoveAbility : MonoBehaviourPun
{
    private Rigidbody rb;
    public float maxPower = 10f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isDragging = false;
    private Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        if (photonView.IsMine)
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
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition;
        float dragDistance = Vector3.Distance(startPosition, endPosition);
        float appliedPower = Mathf.Min(dragDistance * 3, maxPower);
        photonView.RPC(nameof(ApplyForce), RpcTarget.All, forceDirection.normalized * appliedPower);
    }

    [PunRPC]
    void ApplyForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        PhotonView otherPhotonView = other.gameObject.GetComponent<PhotonView>();
        if (otherPhotonView != null && !otherPhotonView.IsMine) // �浹�� ������Ʈ�� �ٸ� �÷��̾��� ������ Ȯ��
        {
            if (PhotonNetwork.IsMasterClient) // ������ Ŭ���̾�Ʈ�� �� ��ȯ�� ����
            {
                PhotonNetwork.LoadLevel("BattleScene"); // ��� Ŭ���̾�Ʈ���� "BattleScene" ������ ��ȯ
            }
        }
    }
}
