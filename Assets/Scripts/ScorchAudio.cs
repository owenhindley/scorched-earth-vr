using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchAudio : MonoBehaviour {

	public static ScorchAudio instance;

	public List<AudioClip> fireSounds;
	public List<AudioClip> impactSounds;
	public AudioSource menuMove;
	public AudioSource menuSelect;

	public AudioSource fireSource;
	public AudioSource impactSource;


	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlayFire(){
		if (instance){
			AudioClip fire = (AudioClip)instance.fireSounds[Random.Range(0, instance.fireSounds.Count)];
			instance.fireSource.PlayOneShot(fire);
		}

	}

	public static void PlayImpact(){
		if (instance){
			AudioClip imp = (AudioClip)instance.impactSounds[Random.Range(0, instance.impactSounds.Count)];
			instance.impactSource.PlayOneShot(imp);
		}

	}

	public static void PlayMenuMove(){
		if (instance){
			instance.menuMove.Play();
		}

	}

	public static void PlayMenuSelect(){
		if (instance){
			instance.menuSelect.Play();
		}

	}
}
