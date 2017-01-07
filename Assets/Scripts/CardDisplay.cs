using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CardDisplay : MonoBehaviour {

	public TextMeshPro title;
	public TextMeshPro posType;
	public TextMeshPro negType;
	public TextMeshPro posValue;
	public TextMeshPro negValue;
	public Image icon;
	public Image background;

	public CardSO cardData;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){

		title.SetText(cardData.name);
		posType.SetText( cardData.positiveType.ToString());
		negType.SetText( cardData.negativeType.ToString());
		posValue.SetText( "+" + cardData.positiveValue);
		negValue.SetText( "-" + cardData.negativeValue);

		icon.sprite = cardData.img;
		background.color = cardData.color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
