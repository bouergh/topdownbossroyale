using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class Manager : NetworkBehaviour {


	public int minPlayers = 1;
	public int maxPlayers = 8;
	private bool gameSetup = false;
	public static Manager instance;
	public GameObject winText;
	
	private void Awake()
     {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
           Destroy(this.gameObject);
           return;//Avoid doing anything else
        }
     
        instance = this;
        DontDestroyOnLoad( this.gameObject );
     }

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
			player.GetComponent<PlayerMovement>().RpcSetCamera();
		}
		
	}

	public void CheckWinConditions(){
		Debug.Log("checking win conditions");
		if(GameObject.FindGameObjectsWithTag("Player").Length == 1 || GameObject.FindGameObjectsWithTag("Boss").Length == 0){
			StartCoroutine(Endgame());
		}
	}

	IEnumerator Endgame(){
		Debug.Log("game has ended !");
		if(!winText) winText = FindObjectOfType<NetworkText>().gameObject; //attention s'il y en a plusieurs !!!
		winText.GetComponent<NetworkText>().textToShow = "Player X wins !";
		yield return new WaitForSeconds(4f);
		NetworkManager.singleton.ServerChangeScene("LobbyScene");
		gameSetup = false;
	}
	
}
