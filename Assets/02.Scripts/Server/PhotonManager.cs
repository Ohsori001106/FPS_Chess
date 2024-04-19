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

/*    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        JoinRandomOrCreateTimeStampedRoom();
    }

    void JoinRandomOrCreateTimeStampedRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No available rooms, creating a new room");
        *//*CreateTimeStampedRoom();*//*
    }*/

    /*void CreateTimeStampedRoom()
    {
        string roomName = "MyRoom_" + System.DateTime.Now.Ticks.ToString();
        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4
        };

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }*/

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room created successfully: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined Room Successfully: {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.LoadLevel("MainScene");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} has left the room.");
        // 여기서 필요한 추가적인 로직을 구현할 수 있습니다.
        // 예: 남은 플레이어 수 확인, 게임 상태 업데이트, 리더보드 업데이트 등
    }
}
