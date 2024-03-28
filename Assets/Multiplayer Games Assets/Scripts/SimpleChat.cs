using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class SimpleChat : NetworkBehaviour
{
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private TMP_Text chatText;

    public void SendChat()
    {
        if (IsServer)
        {
            //Call Client RPC
            ChatClientRPC();
        }
        else if (IsClient)
        {
            //Call Server RPC
            ChatServerRPC();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChatServerRPC()
    {
        chatText.text = "A client says hi";
    }

    [ClientRpc]
    public void ChatClientRPC()
    {
        chatText.text = "Server says hi";
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
