using System.Collections;
using UnityEngine;
using Photon.Pun; // Photon 네트워크 기능을 사용하기 위해 추가

public class AlkagiScene : MonoBehaviourPunCallbacks // PhotonPunCallbacks를 상속받음
{
    void Start()
    {
        // 모든 네트워크 관련 작업은 방에 성공적으로 입장한 후에 실행되어야 합니다.
        // 이를 위해 OnJoinedRoom() 내에서 관련 로직을 처리하도록 변경
    }

    public override void OnJoinedRoom()
    {
        // 방에 입장했을 때 Al 프리팹을 생성
        PhotonNetwork.Instantiate("Al", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
