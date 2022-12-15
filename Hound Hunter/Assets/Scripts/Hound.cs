using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hound : MonoBehaviour {

	public int health = 100;
    public int id;

	public GameObject deathEffect;

	public void TakeDamage (int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die ()
	{
		GameManager.instance.HoundKilled(id);
		Destroy(gameObject);
	}

}