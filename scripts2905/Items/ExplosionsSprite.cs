using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ExplosionsSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public string messageText;

    public GameObject mainSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            OnTaking();
        }    
    }

    public void OnTaking() {
        GameObject.Find("ItemMng").GetComponent<Explosions>().enabled = true;
        //StartCoroutine(GameObject.Find("UIObject").GetComponent<UI>().ChangeTextSlowly(GameObject.Find("UIObject").GetComponent<UI>().itemMessage, messageText, 2));
        GameObject.Find("UIObject").GetComponent<UI>().StartNewMessage(messageText);
        GameObject.Find("Player").GetComponent<MainInventory>().allItems.Add(gameObject);
        Debug.Log(gameObject.name);
        gameObject.SetActive(false);

        
    }
}
