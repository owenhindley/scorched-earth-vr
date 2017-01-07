using UnityEngine;
using System.Collections;

[ScriptOrder(-5)]
public class Sandman : MonoBehaviour
{
	void Awake()
	{
		var agents = Resources.FindObjectsOfTypeAll<SandmanActor>();

		for (int i = 0; i < agents.Length; i++)
		{
			agents[i].Evaluate();
		}
	}
}
