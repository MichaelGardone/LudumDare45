using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    float outerRadius = 2.5f;
    
    [SerializeField]
    Transform droog;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 final = Vector3.zero;
        final.z = -2;

        if (mousePos.x > droog.position.x + outerRadius)
            final.x = droog.position.x + outerRadius;
        else if (mousePos.x < droog.position.x - outerRadius)
            final.x = droog.position.x - outerRadius;
        else
            final.x = mousePos.x;

        if (mousePos.y > droog.position.y + outerRadius)
            final.y = droog.position.y + outerRadius;
        else if(mousePos.y < droog.position.y - outerRadius)
            final.y = droog.position.y - outerRadius;
        else
            final.y = mousePos.y;

        transform.position = final;
    }

}
