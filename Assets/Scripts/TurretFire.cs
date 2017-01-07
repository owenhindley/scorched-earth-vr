using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TurretFire : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float size;
	[Range(0.0f, 1.0f)]
	public float aim;
	[Range(0.0f, 1.0f)]
	public float rate;
	[Range(0, 500)]
	public int multi;
	[Range(0.0f, 1.0f)]
	public float guide;

	public bool isFiring;

	[Range(0.001f, 1.0f)]
	public float minScale = 0.2f;
	[Range(0.001f, 10.0f)]
	public float maxScale = 1.0f;

	[Range(0.001f, 1.0f)]
	public float minFireInterval = 0.2f;
	[Range(0.001f, 10.0f)]
	public float maxFireInterval = 2.0f;

	private float lastFireTime = 0;

	public Transform projectileEmit;
	public GameObject projectileSource;

	// Use this for initialization
	void Start () {
		GetComponentInParent<VRTK_ControllerEvents>().TriggerHairlineStart += new ControllerInteractionEventHandler(DoOnTriggerStart);
		GetComponentInParent<VRTK_ControllerEvents>().TriggerHairlineEnd += new ControllerInteractionEventHandler(DoOnTriggerEnd);
	}
	
	// Update is called once per frame
	void Update () {

		if (isFiring){
			float timeInterval = Mathf.Lerp(minFireInterval, maxFireInterval, Mathf.Lerp(1.0f, 0.0f, rate));

			if (Time.time - lastFireTime > timeInterval) {
				//Firing
				lastFireTime = Time.time;

				Debug.Log ("Fire!");
				for (int i = 0; i < multi; i++) {
					var p = Instantiate (projectileSource, projectileEmit.position, projectileEmit.rotation);
					p.GetComponent<Projectile> ().SetAttributes (Mathf.Lerp(minScale, maxScale, size), aim, guide);
				}
			}
			
		}
	}

	private void DoOnTriggerStart(object sender, ControllerInteractionEventArgs e)
	{
		isFiring = true;
	}

	private void DoOnTriggerEnd(object sender, ControllerInteractionEventArgs e)
	{
		isFiring = false;
	}
}
