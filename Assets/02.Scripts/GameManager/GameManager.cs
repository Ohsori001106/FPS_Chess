using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // �濡 ���� �Ϸ�� �� ���� �Ҵ�
        photonView.RPC("AssignTeam", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void AssignTeam()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // �濡 �ִ� ��� �÷��̾���� ������ Ȯ���ϰ� ���� �Ҵ�
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                Hashtable teamProps = new Hashtable();
                if (i == 0)
                {
                    // ù ��° �÷��̾�� Black �� �Ҵ�
                    teamProps["Team"] = "Black";
                }
                else if (i == 1)
                {
                    // �� ��° �÷��̾�� White �� �Ҵ�
                    teamProps["Team"] = "White";
                }
                players[i].SetCustomProperties(teamProps);
            }
        }
    }
}
