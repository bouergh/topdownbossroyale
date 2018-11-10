using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class Manager : NetworkBehaviour {


	public int minPlayers = 2;
	public int maxPlayers = 2;
	private bool gameSetup = false;
	
	void Start () {
		gameSetup = false;
	}
	
	//Update is called once per frame
	void Update () {

		if(!gameSetup){
			if(GameObject.FindGameObjectsWithTag("Player").Length>=minPlayers){
				StartCoroutine(GameSetup());
				gameSetup = true;
			}
		}
	}

	

	IEnumerator GameSetup(){
		yield return new WaitForSeconds(0.1f);
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
			player.GetComponent<Crosshair>().RpcSpawnCrosshair();
			player.GetComponent<PlayerMovement>().RpcColorPlayer();
		}
		
	}
	
}
