using Photon.Pun;
using UnityEngine;

public class TurnManager : MonoBehaviourPunCallbacks
{
    public int currentPlayerIndex = 0;
    private Photon.Realtime.Player[] players;
    public static TurnManager Instance;
    public PhotonView photonView;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            photonView = GetComponent<PhotonView>();
            if (photonView == null)
            {
                photonView = gameObject.AddComponent<PhotonView>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Successfully joined a room.");
        players = PhotonNetwork.PlayerList;
        Debug.LogError(players.Length);
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateTurn();
        }
    }
    public void LateUpdate()
    {
        players = PhotonNetwork.PlayerList;
        

    }
    void UpdateTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1)%(players.Length );
        photonView.RPC("SetCurrentPlayerIndex", RpcTarget.All, currentPlayerIndex);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        players = PhotonNetwork.PlayerList; // �÷��̾� ��� ������Ʈ
        Debug.Log("Player left: " + otherPlayer.NickName);

        if (players.Length == 0)
        {
            currentPlayerIndex = -1; // �÷��̾ ���� ���
            return;
        }

        // �÷��̾ ������ �� ���� �÷��̾� �ε��� ����
        if (currentPlayerIndex >= players.Length)
        {
            currentPlayerIndex = 0;
        }

        // ������ Ŭ���̾�Ʈ�� �����ִ� ��� �� ������Ʈ
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateTurn();
        }
    }

    [PunRPC]
    public void SetCurrentPlayerIndex(int index)
    {
        currentPlayerIndex = index;
        Debug.LogWarning($"It is now player {currentPlayerIndex}'s turn.");
    }
    [PunRPC]

    public void EndTurn()
    {
        // ��� Ŭ���̾�Ʈ�� EndTurn�� ȣ���� �� ������, ������ ���� ������Ʈ�ϴ� ���� ������ Ŭ���̾�Ʈ������ ����˴ϴ�.
        photonView.RPC("RequestTurnEnd", RpcTarget.MasterClient);
    
    }

    [PunRPC]
    public void RequestTurnEnd()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateTurn();
        }
    }
    public bool IsPlayerTurn(int playerActorNumber)
    {
        if (players == null)
        {
           
            return false;
        }

        return (players[currentPlayerIndex].ActorNumber == playerActorNumber);
    }
}
