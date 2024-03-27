using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    [SerializeField] private TMP_Text joinStatusTxt;
    [SerializeField] private TMP_InputField ipAddress;
    [SerializeField] private UnityTransport transport;

    public void JoinHost()
    {
        NetworkManager.Singleton.StartHost();
        joinStatusTxt.SetText("Joined as Host");
    }

    public void JoinClient()
    {
        //transport.ConnectionData.Address = ipAddress.text.Replace(" ","");
        NetworkManager.Singleton.StartClient();
        joinStatusTxt.SetText("Joined as Client");
    }

    /*
    public void JoinServer()
    {
        NetworkManager.Singleton.StartServer();
        joinStatusTxt.SetText("Joined as Server");
    }
    */

    /*
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    */
}
