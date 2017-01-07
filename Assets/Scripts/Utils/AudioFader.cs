using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class AudioFader : MonoBehaviour {

	private static AudioFader _instance;

	[Tooltip("enable to fade up all AudioSources at Start")]
	public bool fadeInAutomatically = true;
	[Tooltip("Fade duration")]
	public float durationIn = 2.0f;
	public float durationOut = 2.0f;

	[Header("Audio Source list")]
	[Tooltip("A list of all the AudioSources you want fading in/out at scene start/end")]
	public List<AudioSource> audioList = new List<AudioSource>();


	void Awake()
	{
		if (_instance != null)
		{
			Debug.LogError("More than two instances of SceneAudioManager exist. Destroying this one");
			Destroy(this.transform.root.gameObject);
			return;
		}
		else
		{
			//Restore selection 
			_instance = this;
		}
	}

	void OnDestroy()
	{
		if(_instance == this)
		{
			_instance = null;
		}
	}

	// Use this for initialization
	void Start () {
		if (fadeInAutomatically) {
			FadeIn ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static AudioFader Instance
	{
		get {
			if (_instance == null)
				throw new Exception ("Tried to access SceneAudioManager when one doesn't exist in the scene");
			return _instance;
		}

	}

	private void _fadeIn() {

		for (int i = 0; i < audioList.Count; i++) {
			DOTween.Kill (audioList [i]);
			audioList [i].volume = 0.0f;
			audioList [i].DOFade (1.0f, durationIn);
		}

	}

	public static void FadeIn(){
		if (_instance == null) {
			Debug.LogError ("Tried to fade in SceneAudioManager when one doesn't exist in the scene");
			return;
		}
		_instance._fadeIn ();
	}

	private void _fadeOut() {
		for (int i = 0; i < audioList.Count; i++) {
			DOTween.Kill (audioList [i]);
			audioList [i].DOFade (0.0f, durationOut);
		}

	}

	public static void FadeOut()
	{
		if (_instance == null) {
			Debug.LogError ("Tried to fade out SceneAudioManager when one doesn't exist in the scene");
			return;
		}
		_instance._fadeOut ();

	}
}
