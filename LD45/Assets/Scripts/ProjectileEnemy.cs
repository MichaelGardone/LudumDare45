using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileEnemy : EnemyMaster
{

    [SerializeField]
    float movement_speed;

    [SerializeField]
    float seconds_until_next_shot;

    [SerializeField]
    float bullet_speed;

    [SerializeField]
    GameObject bullet_pref;
    // Start is called before the first frame update
    void Start()
    {
        health_bar = gameObject.GetComponent<Slider>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        if (is_dead)
            Die();
    }
    void FollowPlayer()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > distance_til_stop)
        {
            is_attacking = false;
            transform.position += (target.transform.position - transform.position).normalized * movement_speed;
        }
        else
        {
            if(!is_attacking)
            {
                StartCoroutine(StartFiring());
            }
        }
    }

    IEnumerator StartFiring()
    {
        is_attacking = true;
        while (true)
        {
            if (!is_attacking)
                break;
            Vector2 direction = (target.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bullet_pref, transform.position + (Vector3)direction, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet_speed;
            yield return new WaitForSeconds(seconds_until_next_shot);
        }
        
    }
}
