using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;
using TMPro;
using System;

public class SmoothTextChanger : MonoBehaviour
{
    public enum States
    {
        Idle, 
        Visible
    }

    public TextMeshProUGUI textField;
    public float changeDuration;
	public float maxAlpha = 1.0f;

    private StateMachine<States> fsm;
    
    private string targetText;    

    public void SetText(string text)
    {
		if (targetText == text && textField.text == text) return;

        if(fsm == null) fsm = StateMachine<States>.Initialize(this);

        targetText = text;

        if(!string.IsNullOrEmpty(targetText))
        {
            //This will get us nice overwrite behaviour
            fsm.ChangeState(States.Visible, StateTransition.Safe);
        }
        else
        {
            fsm.ChangeState(States.Idle);
        }
    }

    //Reset whenever the text field is hidden
    void OnDisable()
    {
        textField.text = string.Empty;
        targetText = string.Empty;
    }

    IEnumerator Visible_Enter()
    {
		Debug.Log ("CHANGING TEXT TO : " + targetText);
        textField.text = targetText;

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / (changeDuration / 2f);
            textField.alpha = t * maxAlpha;
            yield return null;
        }
    }

    IEnumerator Visible_Exit()
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / (changeDuration / 2f);
			textField.alpha = (1 - t) * maxAlpha;
            yield return null;
        }
    }
}
