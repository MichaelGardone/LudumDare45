using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float maxTimeUntilDisappear = 5.0f;

    [SerializeField]
    float currTime;

    void LateUpdate()
    {
        currTime += Time.deltaTime;

        if (currTime >= maxTimeUntilDisappear)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(1);
            //print("Hit");
            //Deal Damage to Player
            Destroy(gameObject);
        }
    }
}
