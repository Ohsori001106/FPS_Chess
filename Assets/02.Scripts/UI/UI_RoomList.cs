using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class UI_RoomList : MonoBehaviourPunCallbacks
{
    public List<UI_Room> UIRooms;

    public void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
        Clear();
    }
    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    private void Clear()
    {
        foreach (UI_Room roomUI in UIRooms)
        {
            roomUI.gameObject.SetActive(false);
        }
    }

    // ��(��)�� ������ ����(�߰�/����/����)�Ǿ��� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Clear();
        Debug.Log("����������");
        List<RoomInfo> liveRoomList = roomList.FindAll(r => r.RemovedFromList == false);
        int roomCount = liveRoomList.Count;
        for (int i = 0; i < roomCount; ++i)
        {
            UIRooms[i].Set(liveRoomList[i]);
            UIRooms[i].gameObject.SetActive(true);
        }
    }

    public void Roomm(List<RoomInfo> roomList)
    {
        Clear();

        List<RoomInfo> liveRoomList = roomList.FindAll(r => r.RemovedFromList == false);
        int roomCount = liveRoomList.Count;
        for (int i = 0; i < roomCount; ++i)
        {
            UIRooms[i].Set(liveRoomList[i]);
            UIRooms[i].gameObject.SetActive(true);
        }
    }
}