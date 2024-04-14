using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        AssignTeam();
    }

    void AssignTeam()
    {
        string team = Random.Range(0, 2) == 0 ? "Black" : "White";
        Hashtable props = new Hashtable
        {
            { "Team", team }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }
}
