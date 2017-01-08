using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraWait : MonoBehaviour
{

    public LayerMask VRWaitMask;
    private LayerMask ogMasks;

	// Use this for initialization
	void Start ()
	{
	    ogMasks = GetComponentInChildren<Camera>().cullingMask;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetWait(bool enable)
    {
        if (enable)
        {
            GetComponentInChildren<Camera>().cullingMask = VRWaitMask;
        }
        else
        {
            GetComponentInChildren<Camera>().cullingMask = ogMasks;
        }
    }
}
