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
        PhotonNetwork.ConnectUsingSettings(); // ������ ���� Photon ������ ����
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby(); // �⺻ �κ� ����
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
        // ���⼭ �ʿ��� �߰����� ������ ������ �� �ֽ��ϴ�.
        // ��: ���� �÷��̾� �� Ȯ��, ���� ���� ������Ʈ, �������� ������Ʈ ��
    }
}
