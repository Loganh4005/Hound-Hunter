using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public int id;
	public int health = 50;

	public GameObject deathEffect;

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
        Random rnd = new Random();
        int num = rnd.Next(0,10);
        //yield WaitForSeconds(num);
        Move();
    }
}