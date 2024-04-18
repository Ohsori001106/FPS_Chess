using UnityEngine;
using Photon.Pun;

public class CoinSpawner : MonoBehaviourPunCallbacks
{
    public enum SpawnerType { Player1 = 1, Player2, Player3, Player4 }
    public SpawnerType spawnerType;
    public Transform spawnPosition;

    public override void OnJoinedRoom() // 방에 성공적으로 입장했을 때 코인 생성
    {
        SpawnPlayerCoin();
    }

    void SpawnPlayerCoin()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
        if ((int)spawnerType == playerIndex)
        {
            string coinName = $"Player{playerIndex}_Coin";
            GameObject coinPrefab = Resources.Load<GameObject>(coinName);
            if (coinPrefab != null)
            {
                PhotonNetwork.Instantiate(coinName, spawnPosition.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Coin prefab is missing for player " + playerIndex);
            }
        }
    }
}
