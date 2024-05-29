using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour
{
    public GameObject publicExplosion;

    public int explosionChance = 100;
    void OnEnable()
    {
        GameObject.Find("EventMng").GetComponent<EventMng>().OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        GameObject.Find("EventMng").GetComponent<EventMng>().OnEnemyDied -= OnEnemyDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnemyDied(GameObject enemy)
    {
        int randInt = Random.Range(0, 101);
        if (randInt <= explosionChance)
        {
            MakeExplosion(publicExplosion, enemy.transform.position);
        }
    }
    void MakeExplosion(GameObject explosion, Vector2 position) {
        Quaternion quaternion = new Quaternion(0,0,0,0);
        Instantiate(explosion, position, quaternion);
    }
}
