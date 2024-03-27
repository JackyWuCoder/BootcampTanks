using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float tankSpeed = 10.0f;
    [SerializeField] private float tankTurnSpeed = 10.0f;

    GameNetworkManager gameNetworkManager;
    private Rigidbody tankRb;
    private float horizontal;
    private float vertical;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        tankRb = GetComponent<Rigidbody>();
    }

    /*
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    */

    // Update is called once per frame
    private void Update()
    {
        if (!IsOwner) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        tankRb.velocity = tankRb.transform.forward * tankSpeed * vertical;
        tankRb.rotation = Quaternion.Euler(transform.eulerAngles + (transform.up * tankTurnSpeed * horizontal));
    }
}
