using Photon.Pun;
using UnityEngine;

public class BattleSceneManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            string teamType = PhotonNetwork.LocalPlayer.CustomProperties["Team"] as string;
            if (teamType != null)
            {
                string prefabName = teamType == "Black" ? "Pawn Black" : "Pawn White"; // 공백을 포함한 정확한 프리팹 이름 사용
                GameObject spawnPoint = GameObject.Find(teamType + "SpawnPosition");

                if (spawnPoint != null)
                {
                    PhotonNetwork.Instantiate(prefabName, spawnPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Spawn point not found for team: " + teamType);
                }
            }
            else
            {
                Debug.LogError("Team type is not set or is null.");
            }
        }
        else
        {
            Debug.LogWarning("Not connected to Photon or not in a room.");
        }
    }
}
