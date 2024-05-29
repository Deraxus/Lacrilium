using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player") {
            Debug.Log("Игрок в зоне видимости");
            player.gameObject.GetComponent<EnemyLogic>().State = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            player.gameObject.GetComponent<EnemyLogic>().State = 0;
            player.GetComponent<EnemyLogic>().steps = 0;
        }
    }
}
