using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerTest : MonoBehaviour, IPlayerActions
{
    public float speed = 10f;
    public InputMaster controls;

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
        Debug.Log("shot at +"+Time.time);
    }

    public void OnMove(InputAction.CallbackContext context){
        var direction = context.ReadValue<Vector2>();
        Debug.Log("move in "+direction+" at +"+Time.time);
        GetComponent<Rigidbody2D>().velocity = (Vector3)direction*speed;
        
    }
}
