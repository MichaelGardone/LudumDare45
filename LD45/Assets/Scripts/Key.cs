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

    bool timerOn = true;

    private void Start()
    {
        maxTime = Random.Range(8, 12);
    }

    private void Update()
    {
        if(timerOn)
            currTime += Time.deltaTime;

        if(currTime >= maxTime)
        {
            if (callback != null)
                timeOut.Invoke(key);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(callback != null)
                callback.Invoke(key);

            other.GetComponent<PlayerController>().AddHealth(key);

            Destroy(gameObject);
        }
    }

}
