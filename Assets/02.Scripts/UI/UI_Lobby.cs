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
            Debug.Log("�Է��ϼ���.");
            return;
        }

        PhotonNetwork.NickName = nickname;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;   // ���� ������ �ִ� �÷��̾� ��
        roomOptions.IsVisible = true; // �κ񿡼� �� ��Ͽ� ������ ���ΰ�?
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom(roomID, roomOptions, TypedLobby.Default); // ���� �ִٸ� �����ϰ� ���ٸ� ����� ��

    }
   
    

    

}
