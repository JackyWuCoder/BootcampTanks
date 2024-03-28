using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class BasicChat : NetworkBehaviour
{
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private TMP_Text chatText;

    //TO DO: Instead of just posting on the server, update everyone
    //TO DO: Let each chat show up in a new line
    public void SendChat()
    {
        if (IsServer)
        {
            //Call Client RPC
            ChatClientRPC(NetworkManager.Singleton.LocalClientId + ": " + chatInput.text);
        }
        else if (IsClient)
        {
            //Call Server RPC
            ChatServerRPC(NetworkManager.Singleton.LocalClientId + ": " + chatInput.text);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChatServerRPC(string message)
    {
        //chatText.text = "A client says hi";
        if (!IsHost)
        {
            chatText.text += "\n" + message;
        }
        ChatClientRPC(message);
    }

    [ClientRpc]
    public void ChatClientRPC(string message)
    {
        //chatText.text = "Server says hi";
        chatText.text += "\n" + message;
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
