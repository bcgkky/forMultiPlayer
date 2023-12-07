using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class sunucuYonetim : MonoBehaviourPunCallbacks
{
    public Text serverBilgi;
    public InputField kulAdi;
    public InputField odaAdi;
    string kullaniciAdi;
    string odaninAdi;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        /*if (PhotonNetwork.IsConnected)
        {
            serverBilgi.text = "Bağlandı.";
        }*/
        DontDestroyOnLoad(gameObject);
    }
    public void OdaKur()
    {
        SceneManager.LoadScene(1);
        kullaniciAdi = kulAdi.text;
        odaninAdi = odaAdi.text;
        PhotonNetwork.JoinLobby();
    }
    public void GirisYap()
    {
        SceneManager.LoadScene(1);
        kullaniciAdi = kulAdi.text;
        odaninAdi = odaAdi.text;
        PhotonNetwork.JoinLobby();
    }
    public override void OnConnectedToMaster()
    {
        serverBilgi.text = "Bağlandı.";
    }
    public override void OnJoinedLobby()
    {
        if (kullaniciAdi != "" && odaninAdi != "")
        {
            PhotonNetwork.JoinOrCreateRoom(odaninAdi, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinedRoom()
    {
        InvokeRepeating("isimBilgiKontrol", 0, 1f);
        GameObject objem = PhotonNetwork.Instantiate("Oyuncu", new Vector3(0,1f,0), Quaternion.identity,0,null);
        objem.GetComponent<PhotonView>().Owner.NickName = kullaniciAdi;
    }
    void isimBilgiKontrol()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.FindWithTag("alSanaTag").GetComponent<TextMeshProUGUI>().text = "";
            GameObject.FindWithTag("Lavuk_1").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Lavuk_2").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("isimBilgiKontrol");
        }
        else
        {
            //GameObject.Find("oyuncuBekleniyor").GetComponent<TextMeshProUGUI>().text = "Oyuncu Bekleniyor";
            GameObject.FindWithTag("alSanaTag").GetComponent<TextMeshProUGUI>().text = "Oyuncu Bekleniyor";
            GameObject.FindWithTag("Lavuk_1").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Lavuk_2").GetComponent<TextMeshProUGUI>().text = "......";
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        InvokeRepeating("isimBilgiKontrol", 0, 1f);
    }
    void Update()
    {
        if (kullaniciAdi != "" && odaninAdi != "")
        {
            InvokeRepeating("isimBilgiKontrol", 0, 1f);
        }
    }
    public void SkorGuncelle(int sira,int sayiNe)
    {
        switch (sira)
        {
            case 0:
                GameObject.FindWithTag("Lavuk1Skor").GetComponent<TextMeshProUGUI>().text = sayiNe.ToString();
                break;
            case 1:
                GameObject.FindWithTag("Lavuk2Skor").GetComponent<TextMeshProUGUI>().text = sayiNe.ToString();
                break;
        }
        if (sayiNe <= 0)
        {
            if (sira == 0)
            {
                foreach (GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (objem.gameObject.CompareTag("youWin"))
                    {
                        objem.gameObject.SetActive(true);
                        GameObject.FindWithTag("winNer").GetComponent<TextMeshProUGUI>().text = "hll spr dwm 2.lavuk";
                        Time.timeScale = 0;
                    }
                }
            }
            else
            {
                foreach (GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (objem.gameObject.CompareTag("youWin"))
                    {
                        objem.gameObject.SetActive(true);
                        GameObject.FindWithTag("winNer").GetComponent<TextMeshProUGUI>().text = "hll spr dwm 1.lavuk";
                        Time.timeScale = 0;
                    }
                }
            }
        }
    }
}
