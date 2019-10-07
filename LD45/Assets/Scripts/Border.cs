using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    EntityFacing facing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (facing == EntityFacing.NORTH)
            {
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 2, collision.transform.position.z);
                collision.GetComponent<PlayerController>().dashOn = false;
            }
            else if (facing == EntityFacing.EAST)
            {
                collision.transform.position = new Vector3(collision.transform.position.x-2, collision.transform.position.y, collision.transform.position.z);
                collision.GetComponent<PlayerController>().dashOn = false;
            }
            else if (facing == EntityFacing.WEST)
            {
                collision.transform.position = new Vector3(collision.transform.position.x + 2, collision.transform.position.y, collision.transform.position.z);
                collision.GetComponent<PlayerController>().dashOn = false;
            }
            else if (facing == EntityFacing.SOUTH)
            {
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y+2, collision.transform.position.z);
                collision.GetComponent<PlayerController>().dashOn = false;
            }

        }
    }
}
