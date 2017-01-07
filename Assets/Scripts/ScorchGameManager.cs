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
	ChooseCards,
	Win
};

[Serializable]
public struct PlayerGunState
{
	public float Aim;
	public float Size;
	public float Rate;
	public int Multi;
	public float Guidance;
	
}

public class ScorchGameManager : MonoBehaviour {

	private static ScorchGameManager _instance; //Singleton that only lives for the duration of the scene

	public GameStates currentState;

	public Action<GameStates> stateChanged;

	public ChooseCardView cardView;
	public List<CardSO> availableCards;

	public GameObject gun;

	public GameObject desktopCamera;
	public GameObject viveCameraRig;

	public GameObject spawnA;
	public GameObject spawnB;

	public TowerManager playerATowers;
	public TowerManager playerBTowers;

	public PlayerGunState gunStateA;
	public PlayerGunState gunStateB;

	public Players currentPlayer = Players.A;

	private int playerRoundScore = 0;

	[InspectorButton("GotoCards")] public bool gotoCards = false;
	[InspectorButton("GotoReady")] public bool gotoReady = false;
	[InspectorButton("GotoShooting")] public bool gotoShooting = false;
	[InspectorButton("GotoWin")] public bool gotoWin = false;

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

		cardView.CardSelected += OnCardChosen;
		cardView.gameObject.SetActive(false);
	}

	public void GotoCards(){ sm.ChangeState(GameStates.ChooseCards); }
	public void GotoReady(){ sm.ChangeState(GameStates.Ready); }
	public void GotoShooting(){ sm.ChangeState(GameStates.Shooting); }
	public void GotoWin(){ sm.ChangeState(GameStates.Win); }

	//Broadcast to system 
    private void OnStateChanged(GameStates newState)
    {
		Debug.Log ("**** State changed to " + newState);
		currentState = newState;
		stateChanged.Dispatch(newState);
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
		 if (currentPlayer == Players.A){
			 viveCameraRig.transform.position = spawnA.transform.position;
			 viveCameraRig.transform.rotation = spawnA.transform.rotation;
		 }
		 else {
			 viveCameraRig.transform.position = spawnB.transform.position;
			 viveCameraRig.transform.rotation = spawnB.transform.rotation;
		 }

		 // desktop camera show Vive
		 
		 // show countdown

		 // reset round scores (for checking later)
		 playerRoundScore = 0;

		 // change to shooting state
		 DOVirtual.DelayedCall(5.0f, ()=>{
			 sm.ChangeState(GameStates.Shooting);
		 });
	 }
	 void Ready_Update(){}
	 void Ready_Exit(){



	 }

	 // SHOOTING
	 void Shooting_Enter(){

		 // enable gun
		 gun.SetActive(true);

		 // enable tower checking
		 if (currentPlayer == Players.A){
			 playerBTowers.towerNumberChanged += OnTowerNumberChanged;
			 playerATowers.towerNumberChanged -= OnTowerNumberChanged;
		 } else {
			 playerATowers.towerNumberChanged += OnTowerNumberChanged;
			 playerBTowers.towerNumberChanged -= OnTowerNumberChanged;
		 }

	 }
	 void Shooting_Update(){

		 // reduce countdown

		 // when it hits zero, exit state

	 }
	 void Shooting_Exit(){

		 // hide gun
		 gun.SetActive(false);

		// count the number of towers knocked down this round, assign cards accordingly
		int numberCardsToGive = 1;
		numberCardsToGive += playerRoundScore;
		numberCardsToGive = Mathf.Clamp(numberCardsToGive, 1, 5);

		List<CardSO> newCards = new List<CardSO>();
		for (int i=0; i < numberCardsToGive; i++){
			int rndIndex = UnityEngine.Random.Range(0, availableCards.Count);
			newCards.Add(availableCards[rndIndex]);
		}		 
		cardView.SetCards(newCards);
		 
		playerATowers.towerNumberChanged -= OnTowerNumberChanged;
		playerBTowers.towerNumberChanged -= OnTowerNumberChanged;
		
		DOVirtual.DelayedCall(2.0f, ()=>{

			sm.ChangeState(GameStates.ChooseCards);

		});

	 }

	 void OnTowerNumberChanged(int number){
		 playerRoundScore++;
		 if (number == 0){
			 sm.ChangeState(GameStates.Win);
		 }
	 }

	 // CHOOSE CARDS
	 void ChooseCards_Enter(){

		 // desktop camera - show cards view

		 // vr camera - show above battlefield

		 cardView.gameObject.SetActive(true);

	 }
	 void ChooseCards_Update(){}
	 void ChooseCards_Exit(){

		 cardView.gameObject.SetActive(false);

		 if (currentPlayer == Players.A){
			 currentPlayer = Players.B;
		 } else {
			 currentPlayer = Players.A;
		 }

	 }


	 void OnCardChosen(CardSO card){

		 // here is where we apply the new settings
		 if (currentPlayer == Players.A){
			 // normal way around for player A
			 applyEffectToPlayer(ref gunStateA, card.positiveType, card.positiveValue, true);
			 applyEffectToPlayer(ref gunStateA, card.negativeType, card.negativeValue, false);
			 // opposite way around for player B
			 applyEffectToPlayer(ref gunStateB, card.positiveType, card.positiveValue, false);
			 applyEffectToPlayer(ref gunStateB, card.negativeType, card.negativeValue, true);
		 } else {
			  // normal way around for player B
			 applyEffectToPlayer(ref gunStateB, card.positiveType, card.positiveValue, true);
			 applyEffectToPlayer(ref gunStateB, card.negativeType, card.negativeValue, false);
			 // opposite way around for player A
			 applyEffectToPlayer(ref gunStateA, card.positiveType, card.positiveValue, false);
			 applyEffectToPlayer(ref gunStateA, card.negativeType, card.negativeValue, true);
		 }

		 sm.ChangeState(GameStates.Ready);
	 }

	 private void applyEffectToPlayer(ref PlayerGunState state, CardEffectType type, int val, bool positive){
		 switch(type){
			 case CardEffectType.Aim:
			 	state.Aim += val * (positive ? 1.0f : -1.0f);
			 break;
			 case CardEffectType.Guidance:
			 	state.Guidance += val * (positive ? 1.0f : -1.0f);
			 break;
			 case CardEffectType.Multi:
			 	state.Multi += val * (positive ? 1 : -1);
			 break;
			 case CardEffectType.Rate:
			 	state.Rate += val * (positive ? 1.0f : -1.0f);
			 break;
			  case CardEffectType.Size:
			 	state.Size += val * (positive ? 1.0f : -1.0f);
			 break;
		 }
	 }


	 // WINNING
	 void Win_Enter(){

	 }
	 void Win_Update(){
	 }
	 void Win_Exit(){

	 }
	 
	
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
