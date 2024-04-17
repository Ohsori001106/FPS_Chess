using System.Collections;
using UnityEngine;
using Photon.Pun;

public class AlkagiScene : MonoBehaviourPunCallbacks
{
    public Transform Player1_Position; 
    public Transform Player2_Position;
    public Transform Player3_Position;
    public Transform Player4_Position;

    public override void OnJoinedRoom()
    {
        
    }
}
