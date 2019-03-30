using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    
    void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Boss")){
			var hit = collision.gameObject;
			var health = hit.GetComponent<Health>();
			if (health != null)
			{
				health.TakeDamage(10);
			}

		}
        Destroy(gameObject);
    }
}