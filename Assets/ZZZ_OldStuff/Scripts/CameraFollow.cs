using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform playerToFollow;
	void Update () {
		if(playerToFollow){
			transform.position = new Vector3(playerToFollow.position.x, playerToFollow.position.y, transform.position.z);
		}
		
	}
}
