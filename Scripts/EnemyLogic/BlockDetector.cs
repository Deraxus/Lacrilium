using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            Enemy.GetComponent<EnemyLogic>().IsBlock = true;
        }
    }
}
