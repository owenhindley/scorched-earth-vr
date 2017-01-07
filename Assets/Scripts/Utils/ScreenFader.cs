using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class ScreenFader : MonoBehaviour {

	public static float FADE_TIME = 4.0f;

	public Action onComplete;

	public Camera targetCamera;

	private MeshRenderer renderer;

	public bool fadeInAutomatically = true;
	private bool hasFadedIn = false;

	void Awake()
	{
		transform.position = targetCamera.transform.position;
	}
		
	// Use this for initialization
	void Start () {
		hasFadedIn = false;
		transform.position = targetCamera.transform.position;
		renderer = GetComponent<MeshRenderer> ();
		// start fully black?
		renderer.material.color = Color.black;

//		targetMaterial = renderer.material;
//		tempColor = new Color (0.0f, 0.0f, 0.0f, 0.0f);


	}

	public void FadeTo(float alpha, Action callback){
		renderer.material.DOKill ();
		renderer.enabled = true;
		Color c = Color.black;
		c.a = alpha;
		renderer.material.DOColor (c, "_MainColor", FADE_TIME).OnComplete(()=>{
			renderer.enabled = alpha > 0.0f;
			if (callback != null)
				callback.Dispatch();
		});
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeInAutomatically && !hasFadedIn) {
			hasFadedIn = true;
			FadeTo (0.0f, ()=>{});
		}

		// attach to camera
		transform.position = targetCamera.transform.position;


	}
//
//	public void SetTarget(float from, float to){
//
//		Hashtable args = new Hashtable ();
//		args.Add ("from", from);
//		args.Add ("to", to);
//		args.Add ("time", FADE_TIME);
//		args.Add ("easetype", iTween.EaseType.easeInOutQuad);
//		args.Add ("onupdatetarget", gameObject);
//		args.Add ("onupdate", "onTweenUpdate");
//		args.Add ("oncompletetarget", gameObject);
//		args.Add ("oncomplete", "onTweenComplete");
//
//		iTween.ValueTo (gameObject, args);
//
//	}
//
//	void onTweenUpdate(float val){
//		tempColor.a = val;
//		targetMaterial.SetColor ("_MainColor", tempColor);
//		renderer.enabled = val > 0.0f;
//	}

	void onTweenComplete(){

		if (onComplete != null) {
			onComplete.Dispatch ();
		}

	}

	public void InstanceFadeIn(Action callback){
		FadeTo (0.0f, callback);
	}
	public void InstanceFadeOut(Action callback){
		FadeTo (1.0f, callback);
	}

	public void InstanceFadeOutNoCallback(){
		InstanceFadeOut (()=>{});
	}

	public static void FadeIn(Action callback){
		// this is so dodgy...
		GameObject go = GameObject.Find("ScreenFader");
		if (go != null) {
			go.GetComponent<ScreenFader> ().InstanceFadeIn (callback);
		}
	}

	public static void FadeOut(Action callback){
		// this is so dodgy...
		GameObject go = GameObject.Find("ScreenFader");
		if (go != null) {
			go.GetComponent<ScreenFader> ().InstanceFadeOut (callback);
		}
	}

}
