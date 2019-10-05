using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    GameObject target;
    
    // Useless, just need it for SmoothDamp
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 moveTowards = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        
        transform.position = Vector3.SmoothDamp(transform.position, moveTowards, ref velocity, 0.3f);
    }
}
