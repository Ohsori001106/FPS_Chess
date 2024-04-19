using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class CollisionManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider sharedSlider;
    public Slider additionalSlider;
    private Vector3 collisionPoint; // �浹 ������ ������ ����
    PhotonView thisCoinPhotonView;
    PhotonView otherCoinPhotonView;
    private void Awake()
    {
        GameObject slider1Object = GameObject.FindWithTag("Slider1");

        sharedSlider = slider1Object.GetComponent<Slider>();
        GameObject slider2Object = GameObject.FindWithTag("Slider2");

        additionalSlider = slider2Object.GetComponent<Slider>();
    }
    private void Start()
    {
        if (sharedSlider == null || additionalSlider == null)
        {
            Debug.LogError("Sliders are not properly assigned!");
            return;
        }

        // �����̴� UI�� ���� ���� �� ��Ȱ��ȭ
        sharedSlider.gameObject.SetActive(false);
        additionalSlider.gameObject.SetActive(false);

        // �����̴� �� ���� �̺�Ʈ ������ �߰�
        sharedSlider.onValueChanged.AddListener(HandleSliderChange);
        additionalSlider.onValueChanged.AddListener(HandleSliderChange);
    }

    [PunRPC]
    void SyncSliderValue(float value)
    {
        if (!photonView.IsMine)
        {
            // �ٸ� �÷��̾ ���� ȣ��� ���� ���� �����̴� ���� ����
            sharedSlider.value = value;
            additionalSlider.value = value;
        }
    }
    private void HandleSliderChange(float value)
    {
        photonView.RPC("SyncSliderValue", RpcTarget.All, value);
    }

    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �����͸� ������ �÷��̾�
            stream.SendNext(sharedSlider.value);
        }
        else
        {
            // �����͸� �޴� �÷��̾�
            sharedSlider.value = (float)stream.ReceiveNext();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            thisCoinPhotonView = this.gameObject.GetComponent<PhotonView>();
            otherCoinPhotonView = collision.gameObject.GetComponent<PhotonView>();

            if (thisCoinPhotonView != null && otherCoinPhotonView != null)
            {
                if (thisCoinPhotonView.IsMine)
                {
                    sharedSlider.gameObject.SetActive(true);
                    additionalSlider.gameObject.SetActive(true);

                    sharedSlider.value = 50;
                    additionalSlider.value = 50;

                    // �浹 ���� ����
                    collisionPoint = collision.contacts[0].point;

                    photonView.RPC("HandleCoinCollision", RpcTarget.All, thisCoinPhotonView.ViewID, otherCoinPhotonView.ViewID);
                }
            }
        }
    }

    [PunRPC]
    void HandleCoinCollision(int coin1ViewID, int coin2ViewID)
    {
        Debug.Log($"Coin collision detected between {coin1ViewID} and {coin2ViewID}");
        StartCoroutine(SliderInputCoroutine());
    }

    IEnumerator SliderInputCoroutine()
    {
        yield return new WaitForSeconds(3); // 3�� ����

        float endTime = Time.time + 5;

        while (Time.time < endTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (photonView.IsMine)
                {
                    sharedSlider.value += 3;
                    additionalSlider.value -= 3;
                    photonView.RPC("AdjustOpponentSlider", RpcTarget.Others, -3, 3);
                }
            }
            yield return null;
        }

        // 5�ʰ� ���� �� ���� ó��
        DetermineAndApplyCollisionOutcome();
        sharedSlider.gameObject.SetActive(false);
        additionalSlider.gameObject.SetActive(false);
    }

    void DetermineAndApplyCollisionOutcome()
    {
        if (photonView.IsMine)
        {
            // �����̴� �� ��
            if (sharedSlider.value < additionalSlider.value)
            {
                // sharedSlider�� ������ ������ ƨ�ܳ��� ��
                ApplyForceToCoin(photonView.gameObject, -1);
            }
            else
            {
                // additionalSlider�� ������ ������ ƨ�ܳ��� ��
                PhotonView opponentCoinView = otherCoinPhotonView; // �� ������ ������ �����ؾ� ��
                if (opponentCoinView != null)
                {
                    ApplyForceToCoin(opponentCoinView.gameObject, 1);
                }
            }
        }
    }

    void ApplyForceToCoin(GameObject coin, int directionMultiplier)
    {
        Rigidbody rb = coin.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 forceDirection = (coin.transform.position - collisionPoint).normalized;
            float forceMagnitude = 30;
            // ���� �����ϴ� �Լ��� RPC�� ���� ȣ���մϴ�.
            photonView.RPC("ApplyForce", RpcTarget.All, coin.GetComponent<PhotonView>().ViewID, forceDirection, directionMultiplier * forceMagnitude);
        }
    }

    [PunRPC]
    void ApplyForce(int viewID, Vector3 direction, float force)
    {
        PhotonView targetView = PhotonView.Find(viewID);
        if (targetView != null)
        {
            Rigidbody targetRigidbody = targetView.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                targetRigidbody.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }
}
