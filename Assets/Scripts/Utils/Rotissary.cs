using UnityEngine;
using System.Collections;

public class Rotissary : MonoBehaviour
{
    public float frequency; //Hz
    public Vector3 axis;

    // Update is called once per frame
    void Update()
    {
        var rot = axis * frequency * Time.deltaTime * 360f;
        transform.Rotate(rot);
    }
}
