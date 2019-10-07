using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    PISTOL,
    SHOTGUN,
    AK47,
}

public class Gun : MonoBehaviour
{
    [SerializeField]
    float outerRadius = 2.5f;
    
    [SerializeField]
    Transform droog;

    [SerializeField]
    GunType type;
    
    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 final = Vector3.zero;
        final.z = 0;

        if (mousePos.x > droog.position.x + outerRadius)
            final.x = droog.position.x + outerRadius;
        else if (mousePos.x < droog.position.x - outerRadius)
            final.x = droog.position.x - outerRadius;
        else
            final.x = mousePos.x;

        if (mousePos.y > droog.position.y + outerRadius)
            final.y = droog.position.x + outerRadius;
        else if(mousePos.y < droog.position.y - outerRadius)
            final.y = droog.position.x - outerRadius;
        else
            final.y = mousePos.y;

        transform.position = final;

        // Screen pos of gun
        Vector2 objScreenPos = Camera.main.WorldToViewportPoint(droog.position);

        // Screen pos of mouse
        Vector2 mouseScreenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Angle between two positions
        float angle = AngleBetween(mouseScreenPos, objScreenPos);
        
        if(angle <= 145f && angle >= -145f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0, angle));
        }

    }

    float AngleBetween(Vector3 left, Vector3 right)
    {
        return Mathf.Atan2(left.y - right.y, left.x - right.x) * Mathf.Rad2Deg;
    }

}
