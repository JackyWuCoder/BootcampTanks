using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform[] startPositions;

    void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    [SerializeField] private TMP_InputField playerNameField;

    private NetworkObject localPlayer;

    private void Awake()
    {
        Singleton();
    }

    public void SetLocalPlayer(NetworkObject localPlayer)
    {
        this.localPlayer = localPlayer;
        // Set name of player
        if (playerNameField.text.Length > 0)
        {
            localPlayer.GetComponent<PlayerInfo>().SetName(playerNameField.text);
        }
        else
        {
            localPlayer.GetComponent<PlayerInfo>().SetName($"Player {localPlayer.OwnerClientId}");
        }
        playerNameField.gameObject.SetActive(false);
    }

    internal void OnPlayerJoined(NetworkObject networkObject)
    {
        NetworkObject.transform.position = startPositions[(int)networkObject.OwnerClientId].position;
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
