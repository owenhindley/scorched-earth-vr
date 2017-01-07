using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CardDisplay : MonoBehaviour {

	public TextMeshProUGUI title;
	public TextMeshProUGUI posType;
	public TextMeshProUGUI negType;
	public TextMeshProUGUI posValue;
	public TextMeshProUGUI negValue;
	public Image icon;
	public Image background;

	public CardSO cardData;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){

		updateView();

	}

	void updateView(){

		title.SetText(cardData.name);
		posType.SetText( cardData.positiveType.ToString());
		negType.SetText( cardData.negativeType.ToString());
		posValue.SetText( "+" + cardData.positiveValue);
		negValue.SetText( "-" + cardData.negativeValue);

		icon.sprite = cardData.img;
		
		Color c = cardData.color;
		c.a = 1.0f;
		background.color = c;

	}

	public void SetCard(CardSO newData){
		if (newData){
			gameObject.SetActive(true);
			cardData = newData;
			updateView();
		} else{
			gameObject.SetActive(false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
