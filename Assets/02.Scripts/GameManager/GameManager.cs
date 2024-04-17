using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 방에 입장 완료된 후 팀을 할당
            AssignTeams();
        }
    }

    void AssignTeams()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hashtable teamProps = new Hashtable();
            string teamName = "Team " + (i + 1).ToString(); // 팀 이름을 Team 1, Team 2, Team 3, Team 4로 설정
            teamProps["Team"] = teamName;
            players[i].SetCustomProperties(teamProps);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 새로운 플레이어가 방에 입장할 때마다 팀을 다시 할당
            AssignTeams();
        }
    }
}