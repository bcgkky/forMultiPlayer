using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class oyuncu : MonoBehaviour
{

    PhotonView pw;
    public Transform[] nokta;
    int can;
    int hedefLavuk;
    
    void Start()
    {
        pw = GetComponent<PhotonView>();
        can = 10;

        if (pw.IsMine)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;

            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = nokta[0].position;
                hedefLavuk = 1;
            }
            else
            {
                transform.position = nokta[1].position;
                hedefLavuk = 0;
            }
        }
        
    }

    void Update()
    {
        if (pw.IsMine)
        {
            hareket();
            zipla();
            atesEt();

            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.Rotate(Vector3.up * -1f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.Rotate(Vector3.up * 1f);
            }
        }
        //transform.Rotate(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"),0 * 500f * Time.deltaTime);
        
    }
    void atesEt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                if (hit.transform.gameObject.CompareTag("Dusman"))
                {
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("canAzalt", RpcTarget.All, hedefLavuk);

                }
            }
        }
    }
    [PunRPC]
    void canAzalt(int lavukHedef)
    {
        can--;
        GameObject.FindWithTag("Sunucu").GetComponent<sunucuYonetim>().SkorGuncelle(lavukHedef, can);
            
    }

    void hareket()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 20f;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
        transform.Translate(x,0,y);
    }
    void zipla()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, 0);
        }
    }
}
