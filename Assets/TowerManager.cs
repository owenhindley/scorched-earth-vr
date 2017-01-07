using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : MonoBehaviour {

	public Action<int> numberTowers;

	public GameObject towerPrefab;

	public List<TowerInstance> towers;

	public int numberTowers = 5;

	public float radius = 2.0f;
	public float angleSpread = Mathf.PI/2.0f;

	// Use this for initialization
	void Start () {

		// create towers
		float angleInc = angleSpread / numberTowers;
		for (int i=-numberTowers/2; i < numberTowers/2; i++){
			// float newX = 

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
