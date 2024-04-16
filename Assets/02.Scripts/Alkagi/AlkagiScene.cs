using System.Collections;
using UnityEngine;
using Photon.Pun;

public class AlkagiScene : MonoBehaviourPunCallbacks
{
    public Transform blackPawnPosition; // Black 팀의 Pawn이 스폰될 위치
    public Transform whitePawnPosition; // White 팀의 Pawn이 스폰될 위치

    public override void OnJoinedRoom()
    {
        // 방에 입장했을 때 유저의 팀에 따라 적절한 프리팹과 위치에서 생성
        string team = PhotonNetwork.LocalPlayer.CustomProperties["Team"] as string;

        if (team == "Black")
        {
            PhotonNetwork.Instantiate("Pawn Black", blackPawnPosition.position, Quaternion.identity);
        }
        else if (team == "White")
        {
            PhotonNetwork.Instantiate("Pawn White", whitePawnPosition.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("팀 정보가 올바르지 않습니다.");
        }
    }
}
