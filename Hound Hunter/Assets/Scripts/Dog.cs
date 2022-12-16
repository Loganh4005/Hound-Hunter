using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;

    //Grounded Vars
    bool grounded = true;
    SpriteRenderer sprite;
    public int id;
	public int health = 50;

	public GameObject deathEffect;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage (int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}
    public void Update () {
        GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
    }
	void Die ()
	{
		GameManager.instance.DogKilled(id);
		Destroy(gameObject);
	}
    void OnTriggerEnter2D()
    {
        grounded = true;
    }
    void OnTriggerExit2D()
    {
        grounded = false;
    }
    void Move() {

    }
    IEnumerable waitTillMove() {
        
        int num = Random.Range(0,10);
        yield return new WaitForSeconds(num);
        Move();
    }
}