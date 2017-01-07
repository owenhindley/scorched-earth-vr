using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float baseSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity);
		transform.Rotate (Vector3.right, 90);
	}

	public void SetAttributes(float size, float aim, float guide) {
		transform.localScale = new Vector3 (size, size, size);
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Rigidbody> ().velocity += transform.up * (baseSpeed - ((baseSpeed / 2) * size));
	}
}