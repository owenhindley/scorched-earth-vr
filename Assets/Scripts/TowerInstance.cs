using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class TowerInstance : MonoBehaviour {

	public Action<TowerInstance> hit;
	public int index;

	public GameObject cardPrefab;

	[InspectorButton("releaseCard")] public bool doReleaseCard = false;

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag != "Floor"){
			// we were hit
			hit.Dispatch(this);
			Debug.Log("tower hit!");
			ScorchAudio.PlayImpact();
			releaseCard();
		}
	}

	public void releaseCard(){

		GameObject c = (GameObject)UnityEngine.Object.Instantiate(cardPrefab, transform);
		c.transform.parent = transform.parent.parent;
		c.transform.position = transform.position;
		
		c.transform.DOMoveY(c.transform.position.y + 0.2f, 5.0f).SetEase(Ease.Linear);
		c.transform.DOLocalRotate(Vector3.one * Mathf.PI * 3.0f, 5.0f).OnComplete(()=>{

			UnityEngine.Object.Destroy(c);
		}).SetEase(Ease.Linear);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
