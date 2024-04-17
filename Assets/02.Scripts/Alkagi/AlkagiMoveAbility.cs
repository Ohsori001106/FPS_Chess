using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AlkagiMoveAbility : MonoBehaviourPun
{
    private Rigidbody rb;
    private Camera cam;
    private Vector3 dragStartPosition;
    private bool isDragging = false;
    public float maxPower = 10f;

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

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        dragStartPosition = transform.position;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                Vector3 dragEndPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
                Vector3 forceDirection = dragStartPosition - dragEndPosition;
                float dragDistance = Vector3.Distance(dragStartPosition, dragEndPosition);
                float appliedPower = Mathf.Min(dragDistance * 3, maxPower);

                photonView.RPC(nameof(Shoot), RpcTarget.All, forceDirection.normalized * appliedPower);
                isDragging = false;
            }
        }
    }

    [PunRPC]
    void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
}
