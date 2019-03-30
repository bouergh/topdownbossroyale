using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Crosshair : NetworkBehaviour {
	
	[SyncVar]
	private float zPos;
	[SyncVar]public float bulletSpeed;
	public GameObject crosshairPrefab;
	private GameObject crosshair;
	public GameObject bulletPrefab;
	public float safeThresh = 0.2f;
	[SyncVar]
	public float angle = 270;

	// Use this for initialization
	void Start () {
		GetComponent<NetworkAnimator>().animator = GetComponent<Animator>(); //nyeh
		zPos = transform.position.z;
		safeThresh = 1.2f * Mathf.Max(bulletPrefab.GetComponent<Collider2D>().bounds.size.x,bulletPrefab.GetComponent<Collider2D>().bounds.size.y);
	}

    [ClientRpc]
	public void RpcSpawnCrosshair(){
		if(!isLocalPlayer) return;
		Debug.Log(netId+" is spawning his crosshair");
		crosshair = (GameObject)Instantiate(crosshairPrefab, transform.position, transform.rotation);
		//NetworkServer.Spawn(crosshair);
	}

	
	// Update is called once per frame
	[Client]
	void Update () {
		if(!isLocalPlayer || crosshair == null) return;
		crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos - Camera.main.transform.position.z));
		TurnBody();


		if(Input.GetButtonDown("Fire1")){
			//bulletSpawnPos = (Vector2)transform.position + Vector2.right;
			float safeDistance = Mathf.Sqrt(
				GetComponent<Collider2D>().bounds.extents.x*GetComponent<Collider2D>().bounds.extents.x
				+ GetComponent<Collider2D>().bounds.extents.y*GetComponent<Collider2D>().bounds.extents.y
			)*1.3f + safeThresh;
			Vector2 bulletSpawnPos = (crosshair.transform.position - transform.position).normalized*safeDistance + transform.position;
			//Attention dependant du collider, p-e utiliser les Bounds du Collider
			CmdFire(bulletSpawnPos);
		}
	}

	
	[Command]
	public void CmdFire(Vector2 bulletSpawnPos){

		//Debug.Log("server spawning a bullet at position "+bulletSpawnPos);
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawnPos,
            Quaternion.identity);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = (bulletSpawnPos - (Vector2)transform.position) * bulletSpeed;
		//Vector2.right * bulletSpeed;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 3.0f);
    }	

	public void TurnBody(){
		Vector2 lookDir = crosshair.transform.position - transform.position;
		angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg;
		//Debug.Log(angle);
		GetComponent<Animator>().SetFloat("Angle",angle);
	}
}
