using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnemyZone : MonoBehaviour
{
    public bool isOnCollider = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ColliderEnemySpawn")
        {
            isOnCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "ColliderEnemySpawn")
        {
            isOnCollider = false;
        }
    }
}
