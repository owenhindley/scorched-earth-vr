using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkenAim : MonoBehaviour {

	public float speed = 1.0f;
	public float amplitude = 0.01f;
	public int octaves = 3;

	private float wiggleIndex = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		wiggleIndex += speed * Time.deltaTime;
		float angle = 0.0f;
		for (int i=1; i < octaves; i++){
			angle += (wiggleIndex * i)/i;

		}
		transform.RotateAround(Vector3.up, Mathf.Sin(angle) * amplitude);
		transform.RotateAround(Vector3.left, Mathf.Cos(angle * 1.3f) * amplitude);
		
	}
}
