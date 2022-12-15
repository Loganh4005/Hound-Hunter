using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;

    //Grounded Vars
    bool grounded = true;
    SpriteRenderer sprite = GetComponent<SpriteRenderer>();

    void Update () 
    {
        //Jumping
        if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) 
        {
            if(grounded)
            {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jump);
            }
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
        {
            SpriteRenderer.FlipX = false;
            moveVelocity = -speed;
        }
        if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
        {
            moveVelocity = speed;
            SpriteRenderer.FlipX = true;
        }

        GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);

    }
    //Check if Grounded
    void OnTriggerEnter2D()
    {
        grounded = true;
    }
    void OnTriggerExit2D()
    {
        grounded = false;
    }
}