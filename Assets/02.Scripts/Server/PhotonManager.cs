using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Hashtable을 위한 네임스페이스 추가

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
        Debug.Log("서버 접속 성공");
        Debug.Log(PhotonNetwork.CloudRegion);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버 연결 해재");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버 접속 성공");
        Debug.Log($"InLobby?: {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 입장했습니다.");
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
        Debug.Log("방 생성 성공!");
        Debug.Log($"RoomName: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 성공!");
        Debug.Log($"RoomName: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");
        Debug.Log($"MaxCount: {PhotonNetwork.CurrentRoom.MaxPlayers}");
        Debug.Log("Joined room with team: " + PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 생성 실패!");
        Debug.Log(message);
    }
}
