using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float baseSpeed;
	public float magneRadius = 2f;
	private float force;
	private Transform targetTower;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity);
		transform.Rotate (Vector3.right, 90);

		//Guide
		TowerManager enemyTowers;
		var player = ScorchGameManager.Instance.currentPlayer;
		if (player == Players.A) {
			enemyTowers = ScorchGameManager.Instance.playerBTowers;
		} else {
			enemyTowers = ScorchGameManager.Instance.playerATowers;
		}

		if (enemyTowers.towers.Count > 0){

			var i = 0;
			var j = 0;
			foreach (var tower in enemyTowers.towers) {
				if (Vector3.Distance(transform.position, tower.transform.position) < Vector3.Distance(transform.position, enemyTowers.towers[i].transform.position)) {
					i = j;
				}
				j++;
			}
			targetTower = enemyTowers.towers [i].transform;
		
			Vector3 magnetField = targetTower.position- transform.position;
			float index = (magneRadius-magnetField.magnitude)/magneRadius;
			GetComponent<Rigidbody>().AddForce(force*magnetField*index);

		}

		if (transform.position.y < -8f || transform.position.y > 100f) {
			Destroy (gameObject);
		}
		
	}

	public void SetAttributes(float size, float aim, float guide) {
		transform.localScale = new Vector3 (size, size, size);
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Rigidbody> ().velocity += transform.up * (baseSpeed - ((baseSpeed / 1.2f) * size));
	    if (guide >= 0.5)
	    {
	        force = -Mathf.Lerp(-1f, 1f, guide);
	    }
	    else
	    {
	        force = -Mathf.Lerp(-0.5f, 2f, guide);
	    }
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag.Contains("Floor")){
			// boom
			Debug.Log("destroyed projectile");
			Object.Destroy(gameObject);
		}
	}
}