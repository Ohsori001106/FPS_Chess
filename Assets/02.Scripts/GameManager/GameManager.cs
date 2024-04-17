using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Transform blackSpawnPoint; // Black ���� ���� ����Ʈ
    public Transform whiteSpawnPoint; // White ���� ���� ����Ʈ

    void Start()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Team", out object team))
        {
            string teamName = team as string;
            Vector3 spawnPosition = GetSpawnPosition(teamName);
            string prefabName = teamName == "Black" ? "BlackPawn" : "WhitePawn";

            PhotonNetwork.Instantiate(prefabName, spawnPosition, Quaternion.identity);
            Debug.Log($"Spawned {teamName} pawn for {PhotonNetwork.LocalPlayer.NickName} at {spawnPosition}");
        }
        else
        {
            Debug.LogError("Team property is missing for this player.");
        }
    }

    private Vector3 GetSpawnPosition(string teamName)
    {
        // ���� ��ġ�� ���������� �� ������Ʈ���� �����Ͽ� ��ȯ
        return teamName == "Black" ? blackSpawnPoint.position : whiteSpawnPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (ShouldTriggerSceneChange(gameObject, other))
        {
            ChangeScene();
        }
    }

    private bool ShouldTriggerSceneChange(GameObject player, GameObject other)
    {
        return player.CompareTag("Player") && other.CompareTag("Player") && AreFromDifferentTeams(player, other);
    }

    private bool AreFromDifferentTeams(GameObject player1, GameObject player2)
    {
        var team1 = player1.GetComponent<PhotonView>().Owner.CustomProperties["Team"] as string;
        var team2 = player2.GetComponent<PhotonView>().Owner.CustomProperties["Team"] as string;
        return team1 != team2;
    }

    private void ChangeScene()
    {
        PhotonNetwork.LoadLevel("BattleScene");
    }
}
