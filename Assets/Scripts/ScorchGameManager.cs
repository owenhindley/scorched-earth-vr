using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using System;
using DG.Tweening;

public enum Players
{
	A = 0,
	B = 1
};

public enum GameStates
{
	Idle,
	Ready,
	Shooting,
	ChooseCards
};

public class ScorchGameManager : MonoBehaviour {

	private static ScorchGameManager _instance; //Singleton that only lives for the duration of the scene

	public Action<GameStates> stateChanged;

	public GameObject desktopCamera;
	public GameObject viveCameraRig;

	public TowerManager playerATowers;
	public TowerManager playerBTowers;

	public Players currentPlayer = Players.A;

	public StateMachine<GameStates> sm;

	 void Awake()
    {
		if (_instance != null) {
			Debug.LogError("More than two instances of ScorchGameManager exist. Destroying this one");
			Destroy(this.transform.root.gameObject);
			return;
		} else {
			_instance = this;
		}
    }

	// Use this for initialization
	void Start () {
		sm = StateMachine<GameStates>.Initialize(this);

		sm.Changed += OnStateChanged;

	}

	//Broadcast to system 
    private void OnStateChanged(GameStates newState)
    {
		Debug.Log ("**** State changed to " + newState);
		stateChanged(newState);
    }

	// IDLE
	 void Idle_Enter(){

		// desktop camera - randomly above landscape
		// vr camera - randomly above landscape

	 }
	 void Idle_Update(){}
	 void Idle_Exit(){}

	 // READY
	 void Ready_Enter(){

		 // TODO - put VR camera in correct base
		 

	 }
	 void Ready_Update(){}
	 void Ready_Exit(){}

	 // SHOOTING
	 void Shooting_Enter(){}
	 void Shooting_Update(){}
	 void Shooting_Exit(){}

	 // CHOOSE CARDS
	 void ChooseCards_Enter(){}
	 void ChooseCards_Update(){}
	 void ChooseCards_Exit(){}

	 
	
	// Update is called once per frame
	void Update () {
		
	}

	 public static ScorchGameManager Instance
    {
        get
        {
            if (_instance == null) throw new Exception("Please make sure an instance of the ScorchGameManager prefab exists in the scene");
            return _instance;
        }
    }


	void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }

}
