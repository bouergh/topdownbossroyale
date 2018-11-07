﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float speed; //constant velocity of player IF moving
	[SyncVar] private float vert, hori;
	[SyncVar] private Vector3 mov;

	[ClientRpc]
	public void RpcColorPlayer(){
		if(isLocalPlayer) GetComponent<SpriteRenderer>().color = Color.red;
		//if not Color is blue for all enemies (default color of sprite, don't need code)
	}

	void Update(){
		if(!isLocalPlayer) return;
		vert = Input.GetAxisRaw("Vertical");
		hori = Input.GetAxisRaw("Horizontal");
		mov = (new Vector3(hori,vert,0)).normalized*speed;
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
