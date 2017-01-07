using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour {

	public TextMeshPro counterA;
	public TextMeshPro counterB;

	// Use this for initialization
	void Start () {
		
	}

	public void setTime(int min, int sec){
		counterA.SetText(min + ":" + sec.ToString("00"));
		counterB.SetText(min + ":" + sec.ToString("00"));
	}

	public void setReady(int sec){
		if (sec > 0){
			counterA.SetText(sec + "...");
			counterB.SetText(sec + "...");
		} else {
			counterA.SetText("*.*.*");
			counterB.SetText("*.*.*");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
