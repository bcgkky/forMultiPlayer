using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class photonMetodveKodlar : MonoBehaviourPunCallbacks
{
    void Start()
    {
        /*PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.JoinLobby();
        PhotonNetwork.JoinRoom("oda ismi");
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.CreateRoom("oda ismi", oda_ayarlari);
        PhotonNetwork.JoinOrCreateRoom("oda ismi", oda_ayarlari, TypedLobby.Default);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();*/
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Server'a bağlanıldı.");    
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye bağlanıldı");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("odaya bağlanıldı");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("oda'dan çıkıldı.");
    }
    public override void OnLeftLobby()
    {
        Debug.Log("lobi'den çıkıldı.");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("herhangi bir odaya girilemedi.");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("rastgele bir odaya girilemedi.");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("oda oluşturulamadı.");
    }
}
