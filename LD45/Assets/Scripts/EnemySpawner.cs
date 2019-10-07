using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(GameObject gameObject)
    {
        Vector3 spawn = transform.position;
        spawn.z = -1;
        Instantiate(gameObject, spawn, Quaternion.identity);
    }

}
