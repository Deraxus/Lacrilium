using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMng : MonoBehaviour
{
    public delegate void OnEnemyDiedDelegate(GameObject enemy);
    public event OnEnemyDiedDelegate OnEnemyDied;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnEnemyDiedEvent(GameObject enemy) {
        OnEnemyDied?.Invoke(enemy);
    }
}
