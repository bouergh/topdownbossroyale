using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public float movSpeed; //constant velocity of player IF moving
	[SyncVar] private float vert, hori;
	[SyncVar] private Vector2 mov;

	[ClientRpc]
	public void RpcColorPlayer(){
		if(isLocalPlayer) GetComponent<SpriteRenderer>().color = Color.red;
		//if not Color is blue for all enemies (default color of sprite, don't need code)
	}
	[ClientRpc]
	public void RpcSetCamera(){
		if(isLocalPlayer) FindObjectOfType<CameraFollow>().playerToFollow = transform;
	}

	void Update(){
		if(!isLocalPlayer) return;
		vert = Input.GetAxisRaw("Vertical");
		hori = Input.GetAxisRaw("Horizontal");
		mov = (new Vector3(hori,vert,0)).normalized*movSpeed;
	}

	void FixedUpdate()
	{
		if(!isLocalPlayer) return;
		CmdMove(mov);
	}

	[Command]
	void CmdMove(Vector3 mov){ //server does physics, client does input
		RpcMove(mov);
	}

	[ClientRpc]
	void RpcMove(Vector3 mov){
		GetComponent<Rigidbody2D>().velocity = mov;
	}

}
