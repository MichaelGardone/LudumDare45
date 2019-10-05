using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string key;

    public delegate void CallMe(string key);
    public CallMe callback;

    public delegate void TimerOut(string key);
    public TimerOut timeOut;

    float maxTime, currTime;

    private void Start()
    {
        maxTime = Random.Range(6, 8);
    }

    private void Update()
    {
        currTime += Time.deltaTime;

        if(currTime >= maxTime)
        {
            timeOut.Invoke(key);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            callback.Invoke(key);

            other.GetComponent<PlayerController>().AddHealth(key);

            Destroy(gameObject);
        }
    }

}
