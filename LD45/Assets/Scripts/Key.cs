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

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            callback.Invoke(key);

            other.GetComponent<PlayerController>().AddHealth(key);

            Destroy(gameObject);
        }
    }

}
