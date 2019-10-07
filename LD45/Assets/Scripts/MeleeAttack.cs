using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public bool hitPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hitPlayer)
            return;
        if (collision.tag == "Player")
        {
            hitPlayer = true;
            //collision.GetComponent<PlayerController>().TakeDamage(1);
            print("Player Hit");
        }
    }
    
}
