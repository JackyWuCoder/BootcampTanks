using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public ulong clientID;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsServer)
        {
            PlayerDamage other = collision.gameObject.GetComponent<PlayerDamage>();
            if (other != null && clientID != other.OwnerClientId)
            {
                other.GetDamage();
                Debug.Log(OwnerClientId + " is shooting at " + other.OwnerClientId);
            }
        }
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
