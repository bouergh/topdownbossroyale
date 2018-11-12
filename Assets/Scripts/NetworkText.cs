using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkText : NetworkBehaviour {


	[SyncVar(hook = "OnChangeText")]
	public string textToShow = "";

	public void OnChangeText(string tts){
		GetComponent<Text>().text = tts;
	}
}
