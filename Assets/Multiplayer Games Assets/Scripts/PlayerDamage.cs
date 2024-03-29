using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerDamage : NetworkBehaviour
{
    public void GetDamage()
    {
        Debug.Log(OwnerClientId + " is getting hit");
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
