using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public float smooth; 

    public void SetTargetPos(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, smooth * Time.deltaTime);
    }

}
