using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Hashtable�� ���� ���ӽ����̽� �߰�

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.NickName = $"Player1_{UnityEngine.Random.Range(0, 100)}";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.SendRate = 50;
        PhotonNetwork.SerializationRate = 30;
    }

    public override void OnConnected()
    {
        Debug.Log("���� ���� ����");
        Debug.Log(PhotonNetwork.CloudRegion);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� ���� ����");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ���� ����");
        Debug.Log($"InLobby?: {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� �����߽��ϴ�.");
        Debug.Log($"InLobby?: {PhotonNetwork.InLobby}");
        AssignTeamAndJoinRoom();
    }

    private void AssignTeamAndJoinRoom()
    {
        string team = UnityEngine.Random.Range(0, 2) == 0 ? "Black" : "White";
        Hashtable properties = new Hashtable { { "Team", team } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("ChessRoom", options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� ����!");
        Debug.Log($"RoomName: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����!");
        Debug.Log($"RoomName: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");
        Debug.Log($"MaxCount: {PhotonNetwork.CurrentRoom.MaxPlayers}");
        Debug.Log("Joined room with team: " + PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����!");
        Debug.Log(message);
    }
}
