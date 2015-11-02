using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.down, 500 * Time.deltaTime);
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, 500 * Time.deltaTime);
	}
}
