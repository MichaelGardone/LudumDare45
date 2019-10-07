using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    float wave = 1;

    int num_of_enemies = 0;
    int enemies_spawned = 0;
    public static WaveManager instance = null;
    List<EnemySpawner> enemy_spawners  = new List<EnemySpawner>();
    bool levelDone = false;
    [SerializeField]
    GameObject small_melee;
    [SerializeField]
    GameObject big_melee;
    [SerializeField]
    GameObject proj;
    [SerializeField]
    GameObject grey_mine;
    [SerializeField]
    GameObject red_mine;
    [SerializeField]
    GameObject green_mine;
    int small_enemies_perc = 40;
    int big_enemies_perc = 70;
    int ranged_perc = 90;
    int mines_perc = 100;

    int enemies_killed = 0;
    [SerializeField]
    float spawn_per_second;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);

        
    }

    void OnLevelWasLoaded(int level)
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;
        wave++;
        num_of_enemies += 5;
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var t in spawners)
            enemy_spawners.Add(t.GetComponent<EnemySpawner>());
        StartCoroutine(BeginLevel());

    }
    // Start is called before the first frame update
    void Start()
    {
        print("Hi");
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;
        wave++;
        num_of_enemies += 5;
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var t in spawners)
            enemy_spawners.Add(t.GetComponent<EnemySpawner>());
        StartCoroutine(BeginLevel());

    }
    IEnumerator BeginLevel()
    {
        yield return new WaitForSeconds(7);
        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawn_per_second);
            int randomInt = Random.Range(0, enemy_spawners.Count);
            int randomEnemyInt = Random.Range(1, 101);
            GameObject enemy = null;
            if (randomEnemyInt <= small_enemies_perc)
                enemy = small_melee;
            else if (randomEnemyInt <= big_enemies_perc)
                enemy = big_melee;
            else if (randomEnemyInt <= ranged_perc)
                enemy = proj;
            else
            {
                int randomMineInt = Random.Range(1, 4);
                if (randomMineInt == 1)
                    enemy = grey_mine;
                else if (randomMineInt == 2)
                    enemy = green_mine;
                else
                    enemy = red_mine;
            }
            enemy_spawners[randomInt].SpawnEnemy(enemy);
            enemies_spawned++;
            print("Spawn");
            if (enemies_spawned > num_of_enemies)
                break;
            
        }
        

    }

    public void AddToKilled()
    {
        enemies_killed++;
    }
    // Update is called once per frame
    void Update()
    {
        if(levelDone)
        {
            enemies_killed = 0;
            levelDone = false;
            enemies_spawned = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
