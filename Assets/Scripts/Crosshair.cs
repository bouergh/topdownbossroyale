using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Crosshair : NetworkBehaviour {
	
	[SyncVar]
	private float zPos;
	
	public GameObject crosshairPrefab;
	private GameObject crosshair;

	// Use this for initialization
	void Start () {
		zPos = transform.position.z;
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
	}
}
