using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerInstance : MonoBehaviour {

	public Action<TowerInstance> hit;
	public int index;

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag.Contains("Projectile")){
			// we were hit
			hit.Dispatch(this);
			Debug.Log("tower hit!");
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
