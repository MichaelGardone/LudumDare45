using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    private bool starting;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if (!starting)
        {
            StartCoroutine(LoadLevel1());
        }
    }

    private IEnumerator LoadLevel1()
    {
        while (audioSource.volume > 0) {
            audioSource.volume = audioSource.volume - .1f;
            yield return new WaitForSeconds(.1f);
        }
        SceneManager.LoadScene(1);
    }
}
