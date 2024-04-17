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
        Debug.Log("마스터 서버 접속 성공");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 입장했습니다.");
        CreateOrJoinRoom();
    }
    void CreateOrJoinRoom()
    {
        string roomName = "MyRoom";
        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4 // 최대 플레이어 수 설정
        };
        // 방 생성 시도
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 성공!");
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
        Debug.Log($"방 생성 실패! 오류 코드: {returnCode}, 메시지: {message}");
    }

}
