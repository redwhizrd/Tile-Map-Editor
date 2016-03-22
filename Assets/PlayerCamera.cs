
using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	
	public GameObject target;
	//public Camera main;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.transform.position;
	//	main.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.transform.position + offset;
	//	main.transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
	}
}
