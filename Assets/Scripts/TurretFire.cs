using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Random = UnityEngine.Random;

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

	//Aim shake
	//[Range(0.0f, 5f)]
	//public float shakeAmplitude;

	public Transform projectileEmit;
	public GameObject projectileSource;

    public float wiggleSpeed = 1.0f;
    public float wiggleAmplitude = 0.01f;
    public int octaves = 3;

    private float wiggleIndex = 0.0f;

    // Use this for initialization
	void Start () {
		
		GetComponentInParent<VRTK_ControllerEvents>().TriggerHairlineStart += new ControllerInteractionEventHandler(DoOnTriggerStart);
		GetComponentInParent<VRTK_ControllerEvents>().TriggerHairlineEnd += new ControllerInteractionEventHandler(DoOnTriggerEnd);

	}

	void OnEnable(){

		Players currentPlayer = ScorchGameManager.Instance.currentPlayer;
		PlayerGunState newGunState;
		if (currentPlayer == Players.A) {

			newGunState = ScorchGameManager.Instance.gunStateA;
			size = Mathf.InverseLerp (-10f, 10f, newGunState.Size);
			aim = Mathf.InverseLerp (-10f, 10f, newGunState.Aim);
			rate = Mathf.InverseLerp (-10f, 10f, newGunState.Rate);
			multi = Mathf.Clamp(newGunState.Multi, 1, 500);
			guide = Mathf.InverseLerp (-10f, 10f, newGunState.Guidance);

		} else {

			newGunState = ScorchGameManager.Instance.gunStateB;
			size = Mathf.InverseLerp (-10f, 10f, newGunState.Size);
			aim = Mathf.InverseLerp (-10f, 10f, newGunState.Aim);
			rate = Mathf.InverseLerp (-10f, 10f, newGunState.Rate);
			multi = Mathf.Clamp(newGunState.Multi, 1, 500);
			guide = Mathf.InverseLerp (-10f, 10f, newGunState.Guidance);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (isFiring){
			float fireTimeInterval = Mathf.Lerp(minFireInterval, maxFireInterval, Mathf.Lerp(1.0f, 0.0f, rate * 2f));

			if (Time.time - lastFireTime > fireTimeInterval) {
				//Firing
				lastFireTime = Time.time;

				Debug.Log ("Fire!");
				for (int i = 0; i < multi; i++) {
					var p = Instantiate (projectileSource, projectileEmit.position, projectileEmit.rotation);
					p.GetComponent<Projectile> ().SetAttributes (Mathf.Lerp(minScale, maxScale, size), aim, guide);

					//Keep first shot straight
					if (i > 0) {
						p.GetComponent<Rigidbody> ().velocity = Spread (p.GetComponent<Rigidbody> ().velocity, Mathf.Lerp(20f, 12f, size), (float)multi / 2);
					}
				}
			}
		}

		//Aim shake
		/*var randCircle = Random.insideUnitCircle;
		randCircle = Vector2.Scale (randCircle, new Vector2 (Mathf.Lerp(shakeAmplitude, 0, aim), Mathf.Lerp(shakeAmplitude, 0, aim)));
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x + randCircle.x, transform.localEulerAngles.y + randCircle.y, transform.localEulerAngles.z);
*/
	    wiggleIndex += wiggleSpeed * Time.deltaTime;
	    float angle = 0.0f;
	    for (int i=1; i < octaves; i++){
	        angle += (wiggleIndex * i)/i;

	    }
	    transform.RotateAround(Vector3.up, Mathf.Sin(angle) * wiggleAmplitude * (1f - aim));
	    transform.RotateAround(Vector3.left, Mathf.Cos(angle * 1.3f) * wiggleAmplitude * (1f - aim));
	}

	private void DoOnTriggerStart(object sender, ControllerInteractionEventArgs e)
	{
		isFiring = true;
	}

	private void DoOnTriggerEnd(object sender, ControllerInteractionEventArgs e)
	{
		isFiring = false;
	}

	private Vector3 Spread(Vector3 aim, float distance, float variance) {
		aim.Normalize();
		Vector3 v3;
		do {
			v3 = Random.insideUnitSphere;
		} while (v3 == aim || v3 == -aim);
		v3 = Vector3.Cross(aim, v3);
		v3 = v3 * Random.Range(0.0f, variance);
		return aim * distance + v3;
	}
}
