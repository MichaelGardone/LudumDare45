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

    [SerializeField]
    WaveManager wave_manager;

    
    // Start is called before the first frame update
    void Start()
    {
        wave_manager = FindObjectOfType<WaveManager>().GetComponent<WaveManager>();
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
        wave_manager.AddToKilled();
        Destroy(gameObject);
    }
}
