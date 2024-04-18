using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EM_Manager : MonoBehaviour
{
    public Transform enemySpawnParent;
    public List<GameObject> allEnemyPrefabs = new List<GameObject>();
    public List<GameObject> allHazardPrefabs = new List<GameObject>();
    public int enemiesSpawned = 40;
    public int hazardsSpawned = 40;
    public float safeZoneRadius = 10f;
    public float[] enemyRampUp = { 50f, 100f };
    public bool restartOnEscape = true;
    public bool spawnOnStart = true;
    public Transform enemyBox;

    List<GameObject> allSpawnedEnemies = new List<GameObject>();
    List<GameObject> allSpawnedHazards = new List<GameObject>();
    Transform[] allSpawnPoints = new Transform[2];
    Transform depositDoor;

    private void Awake()
    {
        allSpawnPoints[0] = enemySpawnParent.GetChild(0);
        allSpawnPoints[1] = enemySpawnParent.GetChild(1);
    }

    private void Start()
    {
        depositDoor = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
        
        if (spawnOnStart)
            StartRefreshEnemies();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && restartOnEscape)
        {
            RestartEndlessMode();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StartRefreshEnemies();
        }
    }

    public void DestroyAllEnemies ()
    {
        foreach (GameObject index in allSpawnedEnemies)
        {
            Destroy(index);
        }
        allSpawnedEnemies.Clear();

        foreach (GameObject index in allSpawnedHazards)
            Destroy(index);

        allSpawnedHazards.Clear();
    }

    public void StartRefreshEnemies()
    {
        StartCoroutine(RefreshEnemies());
    }

    IEnumerator RefreshEnemies ()
    {
        //Clear out old list and enemies
        float tempListVolume = AudioListener.volume;
        AudioListener.volume = 0;
        DestroyAllEnemies();

        //Spawn all new enemies
        for (int i = 0; i < enemiesSpawned; i++)
        {
            Vector2 spawnPos = new Vector2( 0, 0 );
            do
            {
                float spawnX = Random.Range(allSpawnPoints[0].position.x, allSpawnPoints[1].position.x);
                float spawnY = Random.Range(allSpawnPoints[0].position.y, allSpawnPoints[1].position.y);
                spawnPos = new Vector2(spawnX, spawnY);
            } while (IsVectorOutsideSafeZone(spawnPos) == true);


            
            if (i % 20 == 0)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                GameObject newClone = Instantiate(allEnemyPrefabs[GetEnemyBasedOnRange(spawnPos)], spawnPos, Quaternion.identity);
                allSpawnedEnemies.Add(newClone);
            }
        }

        for (int i = 0; i < hazardsSpawned; i++)
        {
            Vector2 hspawnPos = new Vector2(0, 0);
            do
            {
                float hspawnX = Random.Range(allSpawnPoints[0].position.x, allSpawnPoints[1].position.x);
                float hspawnY = Random.Range(allSpawnPoints[0].position.y, allSpawnPoints[1].position.y);
                hspawnPos = new Vector2(hspawnX, hspawnY);
            } while (IsVectorOutsideSafeZone(hspawnPos) == true);



            if (i % 20 == 0)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                GameObject newHazard = Instantiate(allHazardPrefabs[Random.Range(0, allHazardPrefabs.Count)], hspawnPos, Quaternion.identity);
                allSpawnedHazards.Add(newHazard);
            }
        }

        //Put all the enemies under one parent to keep my heirarchy sane
        foreach (GameObject index in allSpawnedEnemies)
            index.transform.SetParent(enemyBox);

        foreach (GameObject index in allSpawnedHazards)
            index.transform.SetParent(enemyBox);

        

        yield return new WaitForSeconds(2f);
        AudioListener.volume = tempListVolume;
    }

    bool IsVectorOutsideSafeZone (Vector2 potential)
    {
        return potential.x >= depositDoor.position.x - safeZoneRadius && potential.x <= depositDoor.position.x + safeZoneRadius
               && potential.y >= depositDoor.position.y - safeZoneRadius && potential.y <= depositDoor.position.y + safeZoneRadius;
    }

    /// <summary>
    /// Get enemy int based on range from deposit door. Called from RefreshEnemies
    /// </summary>
    /// <param name="spawnPointIndex">Which spawn point am I looking at?</param>
    /// <returns></returns>
    int GetEnemyBasedOnRange (Vector2 spawnPos)
    {
        float dist = Vector2.Distance(depositDoor.position, spawnPos);
        if (dist < enemyRampUp[0]) //0-50
            return 0;
        else if (dist < enemyRampUp[1]) //50-100
            return Random.Range(0, 2);
        else if (dist < enemyRampUp[2])
            return Random.Range(0, 3);
        else //100+
            return Random.Range(0, 4);
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
