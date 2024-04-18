using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        ConnectToPhoton();
    }

    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings(); // 설정에 따라 Photon 서버에 연결
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby(); // 기본 로비에 입장
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        JoinRandomOrCreateTimeStampedRoom();
    }

    void JoinRandomOrCreateTimeStampedRoom()
    {
        // 랜덤 방 참여 시도
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No available rooms, creating a new room");
        CreateTimeStampedRoom();
    }

    void CreateTimeStampedRoom()
    {
        string roomName = "MyRoom_" + System.DateTime.Now.Ticks.ToString(); // 고유한 방 이름 생성
        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4 // 최대 플레이어 수
        };

        // 고유한 방 이름을 사용하여 방 생성 시도
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room created successfully: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined Room Successfully: {PhotonNetwork.CurrentRoom.Name}");
    }

}
