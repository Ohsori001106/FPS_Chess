using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // 방에 입장 완료된 후 팀을 할당
        photonView.RPC("AssignTeam", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void AssignTeam()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 방에 있는 모든 플레이어들의 순서를 확인하고 팀을 할당
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                Hashtable teamProps = new Hashtable();
                if (i == 0)
                {
                    // 첫 번째 플레이어에게 Black 팀 할당
                    teamProps["Team"] = "Black";
                }
                else if (i == 1)
                {
                    // 두 번째 플레이어에게 White 팀 할당
                    teamProps["Team"] = "White";
                }
                players[i].SetCustomProperties(teamProps);
            }
        }
    }
}
