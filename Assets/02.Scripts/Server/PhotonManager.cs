using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.SendRate = 50;
        PhotonNetwork.SerializationRate = 30;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ���� ����");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� �����߽��ϴ�.");
        CreateOrJoinRoom();
    }
    void CreateOrJoinRoom()
    {
        string roomName = "MyRoom";
        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4 // �ִ� �÷��̾� �� ����
        };
        // �� ���� �õ�
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����!");
        Debug.Log($"RoomName: {PhotonNetwork.CurrentRoom.Name}");
        int playerIndex = PhotonNetwork.CurrentRoom.PlayerCount;
        string playerName = $"Player_{playerIndex}";
        PhotonNetwork.NickName = playerName;
        Debug.Log($"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");
        Debug.Log($"NickName: {PhotonNetwork.NickName}");
        Debug.Log($"PlayerIndex: {playerIndex}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"�� ���� ����! ���� �ڵ�: {returnCode}, �޽���: {message}");
    }

}
