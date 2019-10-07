using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float maxTimeUntilDisappear = 5.0f;

    [SerializeField]
    float currTime;

    public float damage = 10f;

    void LateUpdate()
    {
        currTime += Time.deltaTime;

        if (currTime >= maxTimeUntilDisappear)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyMaster>().Take_Damage(damage);
        }

        if(other.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
