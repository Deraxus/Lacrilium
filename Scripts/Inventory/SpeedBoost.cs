using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] float speedKf;
    private float oldSpeedKf;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameObject player = GetComponent<MainItemMng>().player;
        oldSpeedKf = player.GetComponent<Player>().speedKf * speedKf;
        player.GetComponent<Player>().speedKf += speedKf;
        //player.GetComponent<Player>().speed += player.GetComponent<Player>().speedKf * speedKf;
    }

    private void OnDisable()
    {
        GameObject player = GetComponent<MainItemMng>().player;
        player.GetComponent<Player>().speedKf -= oldSpeedKf;
        //player.GetComponent<Player>().speed += player.GetComponent<Player>().speedKf * speedKf;
    }


}
