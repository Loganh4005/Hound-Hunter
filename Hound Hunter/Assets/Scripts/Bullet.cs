using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;

	// Use this for initialization
	void Start () {
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		if (hitInfo.gameObject.layer == 7)
			return;
		if (hitInfo.TryGetComponent<Hound>(out Hound target))
        {
			
			target.TakeDamage(damage);
		} else if (hitInfo.TryGetComponent<Dog>(out Dog target1))
        {
			
			target1.TakeDamage(damage);
		}
		
		

		Instantiate(impactEffect, transform.position, transform.rotation);

		Destroy(gameObject);
	}
	
}