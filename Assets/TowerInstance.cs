using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerInstance : MonoBehaviour {

	public Action hit;

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag.Contains("projectile")){
			// we were hit
			hit();

			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
