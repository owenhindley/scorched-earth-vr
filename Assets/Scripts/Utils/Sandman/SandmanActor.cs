using UnityEngine;
using System.Collections;

public class SandmanActor : MonoBehaviour
{
	public bool awaken;

	public void Evaluate()
	{
		this.gameObject.SetActive(awaken);
	}
}
