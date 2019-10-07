using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMaster : MonoBehaviour
{
    [SerializeField]
    protected float health;

    [SerializeField]
    protected Slider health_bar;
    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected float distance_til_stop;

    protected bool is_attacking = false;

    protected bool is_stunned = false;

    protected bool is_dead = false;


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take_Damage(float amount)
    {
        
        health -= amount;
        if (health <= 0)
            is_dead = true;
    }

    protected void Die()
    {
        WaveManager.instance.AddToKilled();
        Destroy(gameObject);
    }
}
