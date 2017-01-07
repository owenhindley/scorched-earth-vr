using UnityEngine;
using System.Collections;

public class AnchorToTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    
    void Update()
    {
        if(target != null)
        {
            this.transform.position = target.position;
            transform.rotation = target.rotation;
            transform.Translate(offset);
        }
    }
}
