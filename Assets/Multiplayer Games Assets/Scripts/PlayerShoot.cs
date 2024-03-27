using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootSpeed = 5.0f;

    private Rigidbody tankRb;

    public override void OnNetworkSpawn()
    {
        tankRb = GetComponent<Rigidbody>();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetButtonDown("Jump"))    
        {
            Shoot();
        }
    }

    public void Shoot() {
        GameObject bulletObject = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        bulletObject.GetComponent<NetworkObject>().Spawn();
        bulletObject.GetComponent<Rigidbody>().AddForce(tankRb.velocity + (bulletObject.transform.forward * shootSpeed), ForceMode.VelocityChange);
        Destroy(bulletObject, 5);
    }
}
