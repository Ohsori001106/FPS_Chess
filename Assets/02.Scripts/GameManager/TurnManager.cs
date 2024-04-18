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


    [PunRPC]
    public void SetCurrentPlayerIndex(int index)
    {
        currentPlayerIndex = index;
        Debug.LogWarning($"It is now player {currentPlayerIndex}'s turn.");
    }
    [PunRPC]

    public void EndTurn()
    {
        // 모든 클라이언트가 EndTurn을 호출할 수 있지만, 실제로 턴을 업데이트하는 것은 마스터 클라이언트에서만 수행됩니다.
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
            Debug.LogError("Players array is not initialized!");
            return false;
        }

        return (players[currentPlayerIndex].ActorNumber == playerActorNumber);
    }
}
