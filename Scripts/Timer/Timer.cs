using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public float timer = 300;
    public float timerReverse = 0;
    public TMP_Text text;

    public int EnemyMaxCount = 5;
    public float EnemySpawnPeriod = 5;
    private float StartEnemySpawnPeriod;

    private int count = 0;

    public List<GameObject> publicCommonEnemyes;
    public List<GameObject> publicRareEnemyes;
    public List<GameObject> publicEpicEnemyes;
    public List<GameObject> publicBosses;

    private List<GameObject> spawnedRooms;
    public int difficult = 1; // 1 - начало, самый лайт, 2 - сложнее, 3 - тяжелее, 4 - хард, 5 - максимум
    // Start is called before the first frame update
    void Start()
    {
        StartEnemySpawnPeriod = EnemySpawnPeriod;
        //text.text = timer.ToString();
        spawnedRooms = GameObject.Find("RoomGenerator").gameObject.GetComponent<RoomGenerator>().spawned_rooms;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (difficult)
        {
            case 1:
                EnemySpawnPeriod = StartEnemySpawnPeriod * 0.8f;
                break;
            case 2:
                EnemySpawnPeriod = StartEnemySpawnPeriod * 0.6f;
                break;
            case 3:
                EnemySpawnPeriod = StartEnemySpawnPeriod * 0.4f;
                break;
            case 4:
                EnemySpawnPeriod = StartEnemySpawnPeriod * 0.2f;
                break;
            case 5:
                EnemySpawnPeriod = StartEnemySpawnPeriod * 0.1f;
                break;
        }
        timer -= Time.deltaTime;
        timerReverse += Time.deltaTime;
        //float round_timer = Mathf.Round(timer);
        //text.text = round_timer.ToString();
        if (timer < 0) {
            SceneManager.LoadScene(2);
        }

        if (timer < 280)
        {
            difficult = 2;
        }
        if (timer < 260) {
            difficult = 3;
        }


        if (timerReverse >= 0.1f) {
            OneMicSecond();
            timerReverse = 0;
        }
        
    }

    bool CheckRoom(GameObject room)
    {
        if (room.transform.Find("Stats").gameObject.GetComponent<RoomStats>().EnemyCount >= EnemyMaxCount) {
            return false;
        }
        return true;
    }

    bool SpawnEnemy(GameObject room, GameObject enemy) {
        int randCount = room.gameObject.GetComponent<TechSpawner>().EnemySpawenrs.Length;
        randCount = Random.Range(0, randCount);
        Vector3 pos = room.gameObject.GetComponent<TechSpawner>().EnemySpawenrs[randCount].gameObject.transform.position;
        Instantiate(enemy, pos, new Quaternion(0, 0, 0, 0));
        return true;
    }

    bool IsOnCollider(GameObject obj, GameObject collider) {
        if (obj.GetComponent<ColliderEnemyZone>().isOnCollider == true) {
            return true;
        }
        else { return false; }
    }

    GameObject GetRandomRoom(List<GameObject> rooms) {
        int len = rooms.Count;
        int randCount = Random.Range(0, len);
        return rooms[randCount];
    }
    GameObject GetRandomEnemy(List<GameObject> enemyes)
    {
        int len = enemyes.Count;
        int randCount = Random.Range(0, len);
        return enemyes[randCount];
    }

    void FullSpawnEnemy(int difficult = 1) {
        GameObject newRoom, newEnemy;
        int CheckAllRooms = 0;
        for (int i = 0; i <= spawnedRooms.Count - 1; i++)
        {
            if (spawnedRooms[i].transform.Find("Stats").gameObject.GetComponent<RoomStats>().EnemyCount == EnemyMaxCount)
            {
                CheckAllRooms++;
            }
        }
        if (CheckAllRooms == spawnedRooms.Count)
        {
            Debug.Log("Все комнаты в монстрах");
            return;
        }
        CheckAllRooms = 0;
        int randInt = Random.Range(0, 101);
        newEnemy = GetRandomEnemy(publicCommonEnemyes);
        switch (difficult)
        {
            case 1:
                if (randInt <= 90)
                {
                    newEnemy = GetRandomEnemy(publicCommonEnemyes);
                }
                else {
                    newEnemy = GetRandomEnemy(publicRareEnemyes);
                }
                break;
            case 2:
                if (randInt <= 80)
                {
                    newEnemy = GetRandomEnemy(publicCommonEnemyes);
                }
                else if (randInt <= 98)
                {
                    newEnemy = GetRandomEnemy(publicRareEnemyes);
                }
                else {
                    newEnemy = GetRandomEnemy(publicEpicEnemyes);
                }
                break;
            default:
                break;
        }
        while (CheckAllRooms != 1000)
        {
            CheckAllRooms++;
            newRoom = GetRandomRoom(spawnedRooms);
            if (CheckRoom(newRoom) == true)
            {
                SpawnEnemy(newRoom, newEnemy);
                break;
            }
        }
    }

    void OneMicSecond() {
        count += 1;
        if ((count / 10) >= EnemySpawnPeriod)
        {
            Debug.Log("БАНАН");
            FullSpawnEnemy();
            count = 0;
        }
    }
}
