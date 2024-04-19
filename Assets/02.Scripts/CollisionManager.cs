using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class CollisionManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider sharedSlider;
    public Slider additionalSlider;
    private Vector3 collisionPoint; // 충돌 지점을 저장할 변수
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

        // 슬라이더 UI를 게임 시작 시 비활성화
        sharedSlider.gameObject.SetActive(false);
        additionalSlider.gameObject.SetActive(false);

        // 슬라이더 값 변경 이벤트 리스너 추가
        sharedSlider.onValueChanged.AddListener(HandleSliderChange);
        additionalSlider.onValueChanged.AddListener(HandleSliderChange);
    }

    [PunRPC]
    void SyncSliderValue(float value)
    {
        if (!photonView.IsMine)
        {
            // 다른 플레이어에 의해 호출될 때만 로컬 슬라이더 값을 변경
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
            // 데이터를 보내는 플레이어
            stream.SendNext(sharedSlider.value);
        }
        else
        {
            // 데이터를 받는 플레이어
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

                    // 충돌 지점 저장
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
        yield return new WaitForSeconds(3); // 3초 지연

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

        // 5초가 지난 후 로직 처리
        DetermineAndApplyCollisionOutcome();
        sharedSlider.gameObject.SetActive(false);
        additionalSlider.gameObject.SetActive(false);
    }

    void DetermineAndApplyCollisionOutcome()
    {
        if (photonView.IsMine)
        {
            // 슬라이더 값 비교
            if (sharedSlider.value < additionalSlider.value)
            {
                // sharedSlider의 소유자 코인을 튕겨내야 함
                ApplyForceToCoin(photonView.gameObject, -1);
            }
            else
            {
                // additionalSlider의 소유자 코인을 튕겨내야 함
                PhotonView opponentCoinView = otherCoinPhotonView; // 이 변수를 적절히 설정해야 함
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
            // 힘을 적용하는 함수를 RPC를 통해 호출합니다.
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
