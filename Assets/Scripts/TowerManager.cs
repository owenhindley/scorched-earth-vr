using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : MonoBehaviour {

	public Action<int> towerNumberChanged;

	public GameObject towerPrefab;

	public List<TowerInstance> towers = new List<TowerInstance>();

	public int numberTowers = 30;
	public int numberAliveTowers = 30;
	public int numberRows = 5;
	

	public float radius = 2.0f;
	public float angleSpread = Mathf.PI/2.0f;
	public float radiusIncrement = 1.2f;

	public float spacing = 2.0f;

	// Use this for initialization
	void Start () {

		

		// create towers

		float r = radius;
		for (int j = 0; j < numberRows; j++){

			
			int numTowers = (int)(r / spacing);
			float angleInc = angleSpread / numTowers;
		
			for (int i=-numTowers/2; i <= numTowers/2; i++){
				float newX = -r * Mathf.Cos(-i * angleInc);
				float newZ = -r * Mathf.Sin(-i * angleInc);
				GameObject t = (GameObject)UnityEngine.Object.Instantiate(towerPrefab, this.transform);
				t.transform.localPosition = new Vector3(newX, 0, newZ);
				t.SetActive(true);
				TowerInstance ti = t.GetComponent<TowerInstance>();
				ti.hit += OnTowerHit;
				towers.Add(ti);

			}

			r *= radiusIncrement;
		}

		numberTowers = towers.Count;
		numberAliveTowers = numberTowers;


		towerNumberChanged.Dispatch(numberAliveTowers);
		
	}

	void OnTowerHit(TowerInstance ti){
		Debug.Log("TOWER HIT");
		towers.Remove(ti);
		ti.hit -= OnTowerHit;
		numberAliveTowers--;
		towerNumberChanged.Dispatch(numberAliveTowers);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
