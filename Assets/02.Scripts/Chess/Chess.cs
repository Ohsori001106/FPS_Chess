using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Black,
    White
}

[RequireComponent(typeof(ChessAttackAbility))]
[RequireComponent(typeof(ChessRotateAbility))]
[RequireComponent(typeof(ChessMoveAbility))]
public class Chess : MonoBehaviour
{
    public PhotonView PhotonView { get; private set; }
    public Stat Stat;

    private Vector3 _receivedPosition;
    private Quaternion _receivedRotation;

    public State State { get; private set; } = State.Live;
    private void Awake()
    {
        Stat.Init();

        PhotonView = GetComponent<PhotonView>();


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Stat.Health<=0)
        {
            Destroy(gameObject);
        }
    }
}
