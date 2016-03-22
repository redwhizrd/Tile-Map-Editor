using UnityEngine;
using System.Collections;

public class SpringScript : MonoBehaviour {
    bool jumping = false;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    
    void Jump()
    {
        if (!jumping)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * 100);
            jumping = true;
        }
    }
}
