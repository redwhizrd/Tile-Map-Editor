using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Space))
			Jump ();
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().rotation += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().rotation -= 1;
           // Quaternion q = new Quaternion(0, 0, GetComponent<SpringJoint2D>().transform.rotation.z + 1, 0);
         //   GetComponent<SpringJoint2D>().transform.rotation = q;
        }
    }
	void Jump(){
		GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 250);

	}
}

