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
            // �濡 ���� �Ϸ�� �� ���� �Ҵ�
            AssignTeams();
        }
    }

    void AssignTeams()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hashtable teamProps = new Hashtable();
            string teamName = "Team " + (i + 1).ToString(); // �� �̸��� Team 1, Team 2, Team 3, Team 4�� ����
            teamProps["Team"] = teamName;
            players[i].SetCustomProperties(teamProps);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // ���ο� �÷��̾ �濡 ������ ������ ���� �ٽ� �Ҵ�
            AssignTeams();
        }
    }
}