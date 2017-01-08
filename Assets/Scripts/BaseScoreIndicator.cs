using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseScoreIndicator : MonoBehaviour {

	public TextMeshPro counterA;
	public Players whichPlayer;

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {

		if (ScorchGameManager.Instance){

			TowerManager t;
			if (whichPlayer == Players.A){
				t = ScorchGameManager.Instance.playerATowers;
			} else {
				t = ScorchGameManager.Instance.playerBTowers;
			}
			 
			counterA.SetText(t.towers.Count.ToString("00"));

		}
		
	}
}
