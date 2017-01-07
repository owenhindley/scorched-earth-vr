using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ChooseCardView : MonoBehaviour {

	public Action<CardSO> CardSelected;

	public int cardSelectedIndex = 0;

	public List<CardDisplay> cards;


	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){

		SelectCard(cardSelectedIndex);

	}

	public void SetCards(List<CardSO> newCards){
		if (newCards.Count > cards.Count)
		{
			Debug.LogError("Too many new cards?!?");
			return;
		}

		for (var i=0; i < cards.Count; i++){
			if (newCards.Count > i){
				cards[i].gameObject.SetActive(true);
				cards[i].SetCard(newCards[i]);
			} else {
				cards[i].gameObject.SetActive(false);
			}
			
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			cardSelectedIndex--;
			cardSelectedIndex = Mathf.Clamp(cardSelectedIndex, 0, cards.Count);
			SelectCard(cardSelectedIndex);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)){
			cardSelectedIndex++;
			cardSelectedIndex = Mathf.Clamp(cardSelectedIndex, 0, cards.Count);
			SelectCard(cardSelectedIndex);
		}

		if (Input.GetKeyDown(KeyCode.Return)){
			CardSelected(cards[cardSelectedIndex].cardData);
		}
		
	}

	private void SelectCard(int cardIndex){
		for (var i=0; i < cards.Count; i++){
			if (i != cardIndex){
				cards[i].transform.DOKill();
				cards[i].transform.DOScale(Vector3.one * 0.8f, 0.5f);
			} else {
				cards[i].transform.DOKill();
				cards[i].transform.DOScale(Vector3.one * 1.2f, 0.5f);
			}
		}
	}
}
