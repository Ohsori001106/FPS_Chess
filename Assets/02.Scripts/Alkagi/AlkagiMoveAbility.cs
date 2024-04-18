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
                        float distanceToScreen = Vector3.Distance(transform.position, cam.transform.position);
                        startPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
                        isDragging = true;
                        Debug.Log("Start Position: " + startPosition);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                float distanceToScreen = Vector3.Distance(transform.position, cam.transform.position);
                endPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
                isDragging = false;
                Debug.Log("End Position: " + endPosition);
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector3 forceDirection = startPosition - endPosition;
        float dragDistance = Vector3.Distance(startPosition, endPosition);
        float appliedPower = Mathf.Min(dragDistance * 3, maxPower);
        Debug.Log("Force Direction: " + forceDirection + ", Power: " + appliedPower);

        photonView.RPC(nameof(ApplyForce), RpcTarget.All, forceDirection.normalized * appliedPower);
    }

    [PunRPC]
    void ApplyForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
}
