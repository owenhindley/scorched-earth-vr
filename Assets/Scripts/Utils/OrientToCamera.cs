using UnityEngine;
using System.Collections;
using DG.Tweening;

public class OrientToCamera : MonoBehaviour
{

    void OnEnable()
    {
        RotateToCam();
    }

    void Update()
    {
        if(GvrController.Recentered)
        {
            StartCoroutine(RotateAfterFrame());
        }
    }

    private IEnumerator RotateAfterFrame()
    {
        yield return null;
        RotateToCam();
    }

    void RotateToCam()
    {
        var camRotation = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, camRotation.y, 0f);
    }
}
