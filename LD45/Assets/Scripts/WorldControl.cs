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

    [SerializeField]
    List<GameObject> keyPrefabs;

    [SerializeField]
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
            openKeys.Add(s.Trim());
        
        playerBlink = player.GetComponent<PlayerController>().BlinkDist;

        player.GetComponent<PlayerController>().lostKey += AddKey;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        
        if(currTime >= maximumTime)
        {
            maximumTime = Random.Range(maximumTime - minTimeOffset, maximumTime + maxTimeOffset);

            if(openKeys.Count > 0)
            {
                string s = openKeys[Random.Range(0, openKeys.Count)];

                openKeys.Remove(s);

                for (int i = 0; i < keyPrefabs.Count; i++)
                {
                    if (s == keyPrefabs[i].GetComponent<Key>().key)
                    {
                        int rand = new int[] { -1, 1 }[Random.Range(0, 1)];
                        int rand2 = new int[] { -1, 1 }[Random.Range(0, 1)];
                        float x = Random.Range(rand * 10, rand * 30);
                        float y = Random.Range(rand2 * 10, rand2 * 30);

                        Vector3 position = new Vector3(x, y, 0);

                        GameObject g = Instantiate(keyPrefabs[i], position, Quaternion.identity);

                        if (player.GetComponent<PlayerController>().keys.Count == 1)
                        {
                            g.transform.position = new Vector3(player.transform.position.x + 20, player.transform.position.y, -1);
                        }

                        g.GetComponent<Key>().callback += RemoveKey;
                        g.GetComponent<Key>().timeOut += TimedOut;

                        break;
                    }
                }
            }
            
            currTime = 0.0f;
        }

    }

    public void AddKey(string key)
    {
        openKeys.Add(key);
        closedKeys.Remove(key);
    }

    public void TimedOut(string key)
    {
        openKeys.Add(key);
    }

    public void RemoveKey(string key)
    {
        closedKeys.Add(key);
    }
}
