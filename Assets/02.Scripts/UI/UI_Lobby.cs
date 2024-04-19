using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Lobby : MonoBehaviour
{
          

    public InputField NicknameinputFieldUI;
    public InputField RoomIDInputFieldUI;

    private void Start()
    {
        
    }
    public void OnClickMakeRoomButton()
    {
        string nickname = NicknameinputFieldUI.text;
        string roomID = RoomIDInputFieldUI.text;

        if (string.IsNullOrEmpty(nickname)|| string.IsNullOrEmpty(roomID))
        {
            Debug.Log("입력하세요.");
            return;
        }

        PhotonNetwork.NickName = nickname;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;   // 입장 가능한 최대 플레이어 수
        roomOptions.IsVisible = true; // 로비에서 방 목록에 노출할 것인가?
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom(roomID, roomOptions, TypedLobby.Default); // 방이 있다면 입장하고 없다면 만드는 것

    }
   
    

    

}
