using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    public float timer;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

    }

}
