using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public RectTransform healthBar;
	public bool destroyOnDeath;

	public const int maxHealth = 100;
	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			Debug.Log("Dead!");
			if (destroyOnDeath)
			{//for the boss
				CmdDestroy(gameObject);
			} 
			else
			{//players
				currentHealth = maxHealth;
				// called on the Server, but invoked on the Clients
				RpcRespawn(); 
			}
		}
	}

	[ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
			// Set the player’s position to a spawn point from Network Manager
			//transform.position = NetworkManager.singleton.GetStartPosition().position;;
			Debug.Log("player "+netId+" is dead !");
			CmdDestroy(gameObject);
        }
    }

	[Command]
	void CmdDestroy(GameObject go){
		NetworkServer.Destroy(gameObject);
		Manager.instance.CheckWinConditions();
	}

	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
}
