using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Manager : NetworkManager {


	public int minPlayers = 2;
	public int maxPlayers = 4;
	private bool gameStarted = false, gameSetup = false;
	
	void Start () {
		matchSize = (uint)maxPlayers;
	}
	
	//Update is called once per frame
	void Update () {
		if(NetworkManager.singleton.numPlayers>=minPlayers && NetworkManager.singleton.numPlayers<maxPlayers){
			if(!gameStarted) {
				gameStarted = true;
				Debug.Log("game started");
			}
		}else{
			if(gameStarted){
			gameStarted = false;
			gameSetup = false;
			Debug.Log("game stopped");
			}				
		}

		if(gameStarted && !gameSetup){
			Debug.Log("spawning the crosshairs for each player");
			GameSetup();
		}
	}

	void GameSetup(){
	
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
			player.GetComponent<Crosshair>().RpcSpawnCrosshair();
			player.GetComponent<PlayerController>().RpcColorPlayer();
		}
		gameSetup = true;
	}
	
}
