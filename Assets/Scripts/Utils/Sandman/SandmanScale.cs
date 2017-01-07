using UnityEngine;
using System.Collections;

public class SandmanScale : MonoBehaviour
{
	public Vector3 defaultScale;

	public void Awake()
	{
		transform.localScale = defaultScale;
	}
}
