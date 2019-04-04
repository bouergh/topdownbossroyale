using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerTest : MonoBehaviour, IPlayerActions
{
    public float speed = 10f;
    public InputMaster controls;
    public Transform crosshair;
    public GameObject bulletPrefab;
    public float baseSafeDistance = .2f;

    public float shootCd = .2f;
    private float shootTimer = 0f;

    public void FixedUpdate()
    {
        shootTimer += Time.fixedDeltaTime;
    }

    public void OnEnable()
    {
        controls.Player.Enable();
    }

    public void OnDisable()
    {
        controls.Player.Disable();
    }
    void Awake()
    {
        controls.Player.SetCallbacks(this);
    }

    // Update is called once per frame
    public void OnShoot(InputAction.CallbackContext context){
        //Debug.Log("shot at +"+Time.time);
        if(shootTimer >= shootCd)
            ThrowProjectile(transform.position, crosshair.position);
    }

    public void OnMove(InputAction.CallbackContext context){
        var direction = context.ReadValue<Vector2>();
        //Debug.Log("move in "+direction+" at +"+Time.time); ///will really slow down the game because every frame
        GetComponent<Rigidbody2D>().velocity = (Vector3)direction*speed;
        
    }

    public void OnAim(InputAction.CallbackContext context){
        var pos = context.ReadValue<Vector2>();
        //Debug.Log("crosshair in "+pos+" at +"+Time.time); ///will really slow down the game because every frame
        pos = Camera.main.ScreenToWorldPoint(new Vector2(pos.x, pos.y));
        crosshair.position = pos;
    }

    public void ThrowProjectile(Vector2 origin, Vector2 destination){

        shootTimer = 0f;

        float safeDistance = Mathf.Sqrt(
				GetComponent<Collider2D>().bounds.extents.x*GetComponent<Collider2D>().bounds.extents.x
				+ GetComponent<Collider2D>().bounds.extents.y*GetComponent<Collider2D>().bounds.extents.y
			)*1.3f + baseSafeDistance;
		Vector2 bulletSpawnPos = (destination - origin).normalized*safeDistance + origin;
        
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawnPos,
            Quaternion.identity);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = (destination - origin) * bullet.GetComponent<Bullet>().speed;
    }
}
