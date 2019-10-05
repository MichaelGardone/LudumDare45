using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldControl : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float maximumTime;

    [SerializeField]
    float maxTimeOffset, minTimeOffset;

    float currTime = 0.0f;

    List<string> openKeys   = new List<string>();
    List<string> closedKeys = new List<string>();

    float playerBlink;

    private void Start()
    {
        maximumTime = Random.Range(maximumTime - minTimeOffset, maximumTime + maxTimeOffset);

        TextAsset txtAsset = (TextAsset)Resources.Load("keys", typeof(TextAsset));
        string[] keyFile = txtAsset.text.Split(new char[] { '\n' });

        foreach (string s in keyFile)
            openKeys.Add(s);
        
        playerBlink = player.GetComponent<PlayerController>().BlinkDist;
    }

    // Update is called once per frame
    void Update()
    {

        currTime += Time.deltaTime;

        if(currTime >= maximumTime)
        {
            maximumTime = Random.Range(maximumTime - minTimeOffset, maximumTime + maxTimeOffset);

            string s = openKeys[Random.Range(0, openKeys.Count)];

            openKeys.Remove(s);
        }

    }

    public void RemoveKey(string key)
    {
        closedKeys.Add(key);
    }
}
