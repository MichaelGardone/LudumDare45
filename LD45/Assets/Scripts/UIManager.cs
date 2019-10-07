using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    Text score;
    Text wave;
    int current_wave = 0;
    int enemies_killed = 0;
    [SerializeField]
    bool fade_out = false;
    [SerializeField]
    bool fade_in = false;
    bool wave_number_shown = true;
    private void Awake()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        wave = GameObject.Find("Wave").GetComponent<Text>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);


    }
    // Start is called before the first frame update
    void Start()
    {
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Enemies Killed: " + enemies_killed;

        if (wave.color.a >= 1.0f && fade_in)
            StartCoroutine(WaitFade());
        if (wave.text == "Wave Completed" && fade_out)
            NextWave();
        if(wave.text == "Game Over" && fade_out)
        {
            SceneManager.LoadScene(0);
            WaveManager.instance = null;
            Destroy(gameObject);
        }
    }   

    public void IncreaseScore()
    {
        enemies_killed++;
    }

    void GameOver()
    {
        wave.text = "Game Over";
        StartCoroutine(FadeTextToFullAlpha(1, wave));
    }

    public void NextWave()
    {
        current_wave++;
        wave.text = "Wave " + current_wave.ToString();
        print(wave.text);
        StartCoroutine(FadeTextToFullAlpha(1, wave));
    }
    public void WaveCompleted()
    {
        wave.text = "Wave Completed";
        StartCoroutine(FadeTextToFullAlpha(1, wave));
    }

    public IEnumerator WaitFade()
    {
        print("Wait");
        fade_in = false;
        fade_out = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeTextToZeroAlpha(1, wave));

    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        fade_out = false;
        print("Fade in");
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        fade_in = true;
        
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        print("Fade Out");
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        fade_out = true;
    }
}
