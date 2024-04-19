using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUpZone : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public float publicKoef = 1f;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "Player") {
            Debug.Log("пеир");
            UpRate(publicKoef);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "Player")
        {
            DownRate(publicKoef);
        }
    }

    private void UpRate(float koef) {
        player.GetComponent<Player>().rateKf += publicKoef;
    }

    private void DownRate(float koef)
    {
        player.GetComponent<Player>().rateKf -= publicKoef;
    }
}
