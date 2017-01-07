using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float baseSpeed;
	public Transform tower;
	private float radius = 5f;
	private float force;
	// Use this for initialization
	void Start () {
		tower = GameObject.Find ("BaseA").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity);
		transform.Rotate (Vector3.right, 90);

		//Guide
		Vector3 magnetField = tower.position- transform.position;
		float index = (radius-magnetField.magnitude)/radius;
		GetComponent<Rigidbody>().AddForce(force*magnetField*index);
	}

	public void SetAttributes(float size, float aim, float guide) {
		transform.localScale = new Vector3 (size, size, size);
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Rigidbody> ().velocity += transform.up * (baseSpeed - ((baseSpeed / 2) * size));
		force = -guide;
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag.Contains("ProjectileDestroyer")){
			// boom
			Debug.Log("destroyed projectile");
			Object.Destroy(gameObject);
		}
	}
}