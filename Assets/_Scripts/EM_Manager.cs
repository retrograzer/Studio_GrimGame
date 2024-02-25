using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EM_Manager : MonoBehaviour
{
    public GameObject[] enemies;
    public int waveNum = 0;
    public float waveDelay = 3f;
    public TextMeshProUGUI waveText, enemyCountText;
    public GameObject gemCrate;
    public GameObject heartCrate;

    GameObject[] spawnPointList;
    GameObject[] gemSpawnList;
    GameObject[] heartSpawnList;
    int enemiesRemaining = 100;
    float cooldown = 0f;
    int enemyCountPerWave = 25;


    // Start is called before the first frame update
    void Start()
    {
        spawnPointList = GameObject.FindGameObjectsWithTag("CollSpawnPoint");
        gemSpawnList = GameObject.FindGameObjectsWithTag("GCrateSpawn");
        heartSpawnList = GameObject.FindGameObjectsWithTag("HCrateSpawn");
        Debug.Log("Spawn List: " + spawnPointList.ToString());
        StartWave();
        SpawnPickups();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void StartWave()
    {
        enemiesRemaining = enemyCountPerWave;
        enemyCountText.text = "Enemies " + enemiesRemaining;
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        SpawnEnemies();
        if (waveNum % 10 == 0 && waveNum != 0)
            SpawnPickups();
        WaveTickCount();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCountPerWave; i++)
        {
            GameObject clone = Instantiate(enemies[0], spawnPointList[Random.Range(0, spawnPointList.Length - 1)].transform.position, Quaternion.identity);
        }
    }

    void SpawnPickups()
    {
        foreach (GameObject index in gemSpawnList)
        {
            GameObject gemClone = Instantiate(gemCrate, index.transform.position, Quaternion.identity);
        }

        foreach (GameObject gindex in heartSpawnList)
        {
            GameObject heartClone = Instantiate(heartCrate, gindex.transform.position, Quaternion.identity);
        }
    }

    void WaveTickCount()
    {
        waveNum++;
        waveText.text = "Wave " + waveNum;
        enemyCountPerWave += 5;
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
        Debug.Log("ER: " + enemiesRemaining);
        enemyCountText.text = "Enemies " + enemiesRemaining;
        if (enemiesRemaining <= 0)
        {
            Debug.Log("New Wave");
            StartWave();
        }
    }

    public void RestartEndlessMode ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
