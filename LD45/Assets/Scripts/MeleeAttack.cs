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
        print(collision);
        if (collision.tag == "Player")
        {
            hitPlayer = true;
            //Code Needed To Hit Player
            print("Player Hit");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);

    }
}
