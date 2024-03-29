using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [SerializeField] private TMP_Text playerNameTxt;

    // Variable to hold the name of the player

    public NetworkVariable<FixedString64Bytes> playerName = new NetworkVariable<FixedString64Bytes>(
        new FixedString64Bytes("Player name"),
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        playerName.OnValueChanged += OnNameChanged;
        playerNameTxt.SetText(playerName.Value.ToString());
        gameObject.name = "Player_" + playerName.Value.ToString();
        if (IsLocalPlayer)
        {
            GameManager.Instance.SetLocalPlayer(NetworkObject);
        }
        GameManager.Instance.OnPlayerJoined(NetworkObject);
    }

    private void OnNameChanged(FixedString64Bytes preVal, FixedString64Bytes newVal)
    {
        if (newVal != preVal)
        {
            playerNameTxt.SetText(newVal.Value);
        }
    }

    public void SetName(string name)
    {
        playerName.Value = new FixedString64Bytes(name);
    }

    public override void OnNetworkDespawn()
    {
        playerName.OnValueChanged -= OnNameChanged;
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
