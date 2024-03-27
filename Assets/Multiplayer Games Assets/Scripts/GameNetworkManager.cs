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

    [SerializeField] private TMP_InputField playerNameInput;

    public void JoinHost()
    {
        NetworkManager.Singleton.StartHost();
        joinStatusTxt.SetText(playerNameInput.text + " joined the game as a Host");
    }

    public void JoinClient()
    {
        //transport.ConnectionData.Address = ipAddress.text.Replace(" ","");
        NetworkManager.Singleton.StartClient();
        joinStatusTxt.SetText(playerNameInput.text + " joined the game as a Client");
    }

    /*
    public void JoinServer()
    {
        NetworkManager.Singleton.StartServer();
        joinStatusTxt.SetText("Joined as Server");
    }
    */

    public string GetPlayerNameInput() 
    {
        return playerNameInput.text;
    }

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
