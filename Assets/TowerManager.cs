using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : MonoBehaviour {

	public Action<int> towerNumberChanged;

	public GameObject towerPrefab;

	public List<TowerInstance> towers;

	public int numberTowers = 5;

	public float radius = 2.0f;
	public float angleSpread = Mathf.PI/2.0f;

	// Use this for initialization
	void Start () {

		// create towers
		float angleInc = angleSpread / numberTowers;
		for (int i=-numberTowers/2; i <= numberTowers/2; i++){
			float newX = -radius * Mathf.Cos(-i * angleInc);
			float newZ = -radius * Mathf.Sin(-i * angleInc);
			GameObject t = (GameObject)UnityEngine.Object.Instantiate(towerPrefab, this.transform);
			t.transform.localPosition = new Vector3(newX, 0, newZ);
			t.SetActive(true);

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
