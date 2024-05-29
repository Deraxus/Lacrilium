using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBootsSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTaking() {
        GameObject.Find("ItemMng").GetComponent<SpeedBoost>().enabled = true;
        GameObject.Find("Player").GetComponent<MainInventory>().allItems.Add(gameObject);
    }
}
