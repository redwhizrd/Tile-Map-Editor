
using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public float Speed = 2f;
	private float movex = 0f;
	private float movey = 0f;
	public Rigidbody2D rocket;
	public float speed = 10f;
	private bool jumping;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.A))
			movex = -1;
		else if (Input.GetKey (KeyCode.D))
			movex = 1;
		else
			movex = 0;
		if (Input.GetKey (KeyCode.W) && !jumping)
			Jump ();
		else
			movey = 0;
		if (Input.GetKey (KeyCode.Space))
			FireBullet ();

	}
	
	void FixedUpdate ()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (movex * Speed, movey * Speed);
	}
	
	void FireBullet(){

		Rigidbody2D rocketClone = (Rigidbody2D) Instantiate(rocket, transform.position + new Vector3(2,0), transform.rotation);
		Physics.IgnoreCollision (rocketClone.GetComponent<Collider> (), GetComponent<Collider> ());
		rocketClone.velocity = transform.right * speed;

	}

	void Jump(){
		if (movey != 1) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 500);

		}
	}
} 