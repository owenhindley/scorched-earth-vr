using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float size;
	[Range(0.0f, 1.0f)]
	public float aim;
	[Range(0.0f, 1.0f)]
	public float rate;
	[Range(0.0f, 1.0f)]
	public float multi;

	public bool isFiring;

	[Range(0.001f, 1.0f)]
	public float minScale = 0.2f;
	[Range(0.001f, 10.0f)]
	public float maxScale = 1.0f;

	[Range(0.001f, 1.0f)]
	public float minFireInterval = 0.2f;
	[Range(0.001f, 10.0f)]
	public float maxFireInterval = 2.0f;

	[Range(0, 10)]


	private float lastFireTime = Time.time;

	public GameObject projectileSource;
	public Transform projectileEmitPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFiring){

			float timeInterval = Mathf.Lerp(minFireInterval, maxFireInterval, Mathf.InverseLerp(0.0f, 1.0f, rate));
			if (Time.time - lastFireTime > timeInterval){
				// FIRING!
				lastFireTime = Time.time;

				float 

				


			}

		}
	}

	IEnumerable doFire(int numToFire, float fireDelay){
		while(numToFire > 0){
			GameObject p = (GameObject)Object.Instantiate(projectileSource, projectileEmitPoint);
			float projectileScale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(0.0f, 1.0f, size));

			yield return new WaitForSeconds(fireDelay);
		}
		yield return null;
	}
}
