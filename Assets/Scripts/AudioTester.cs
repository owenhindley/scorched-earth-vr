using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour {

	[InspectorButton("Fire")] public bool doFire;
	[InspectorButton("Impact")] public bool doImpact;
	[InspectorButton("MenuMove")] public bool doMove;
	[InspectorButton("MenuSelect")] public bool doSelect;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire(){
		ScorchAudio.PlayFire();
	}

	public void Impact(){
		ScorchAudio.PlayImpact();
	}

	public void MenuMove(){
		ScorchAudio.PlayMenuMove();
	}

	public void MenuSelect(){
		ScorchAudio.PlayMenuSelect();
	}
}
