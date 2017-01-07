using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName="CardSO", menuName="ScorchVR/Card", order = 1)]
public class CardSO : ScriptableObject {


	public string cardName;

	public CardEffectType positiveType;
	public int positiveValue = 0;

	public CardEffectType negativeType;
	public int negativeValue = 0;
	

	public CardSO(){


	}
}

[Serializable]
public enum CardEffectType {
	Aim = 0,
	Size = 1,
	Rate = 2,
	Multi = 3 
}
