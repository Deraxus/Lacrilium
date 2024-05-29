using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[10];
    public List<GameObject> passiveItems = new List<GameObject>();
    public int currentCursor = 0; // 10 €чеек инвентар€
    public List<GameObject> old_items = new List<GameObject>();

    public List<GameObject> allHands = new List<GameObject>();
    public List<GameObject> allItems = new List<GameObject>();

    public bool canSwap = true;
    private string currentItem = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1") && canSwap == true) { 
            currentCursor = 0;
        }
        else if (Input.GetKey("2") && canSwap == true)
        {
            currentCursor = 1;
        }
        else if (Input.GetKey("3") && canSwap == true)
        {
            currentCursor = 2;
        }
        /*for (int i = 0; i <= inventory.Length - 1; i++)
        {
            if (i != currentCursor) {
                inventory[i].SetActive(false);
            }
        }*/
        switch (inventory[currentCursor].name) {
            case "weapon_blaster":
                /*if (InventoryClear(inventory[currentCursor].name) == false) {
                    break;
                }
                GameObject obj1 = transform.Find("hand1_blaster").gameObject;
                GameObject obj2 = transform.Find("hand2_blaster").gameObject;
                foreach (Transform child in obj1.transform)
                    child.gameObject.SetActive(true);
                foreach (Transform child in obj2.transform)
                    child.gameObject.SetActive(true);
                obj1.GetComponent<SpriteRenderer>().enabled = true;
                obj2.GetComponent<SpriteRenderer>().enabled = true;
                //old_items.Add(obj1);
                //old_items.Add(obj2);
                break;*/
                if (currentItem != "weapon_blaster")
                {
                    InventoryClear("");
                    GameObject obj1 = transform.Find("hand1_blaster").gameObject;
                    GameObject obj2 = transform.Find("hand2_blaster").gameObject;
                    obj1.GetComponent<SpriteRenderer>().enabled = true;
                    obj2.GetComponent<SpriteRenderer>().enabled = true;
                    foreach (Transform child in obj1.transform)
                        child.gameObject.SetActive(true);
                    foreach (Transform child in obj2.transform)
                        child.gameObject.SetActive(true);
                    currentItem = "weapon_blaster";
                }
                break;
            case "weapon_pistol":
                if (currentItem != "weapon_pistol") {
                    InventoryClear("");
                    GameObject obj1 = transform.Find("hand1_pistol").gameObject;
                    GameObject obj2 = transform.Find("hand2_pistol").gameObject;
                    obj1.GetComponent<SpriteRenderer>().enabled = true;
                    obj2.GetComponent<SpriteRenderer>().enabled = true;
                    foreach (Transform child in obj1.transform)
                        child.gameObject.SetActive(true);
                    foreach (Transform child in obj2.transform)
                        child.gameObject.SetActive(true);
                    currentItem = "weapon_pistol";
                }
                break;
            case "weapon_ak":
                if (currentItem != "weapon_ak")
                {
                    InventoryClear("");
                    GameObject obj1 = transform.Find("hand1_ak").gameObject;
                    GameObject obj2 = transform.Find("hand2_ak").gameObject;
                    obj1.GetComponent<SpriteRenderer>().enabled = true;
                    obj2.GetComponent<SpriteRenderer>().enabled = true;
                    foreach (Transform child in obj1.transform)
                        child.gameObject.SetActive(true);
                    foreach (Transform child in obj2.transform)
                        child.gameObject.SetActive(true);
                    currentItem = "weapon_ak";
                }
                break;

        }
    }


    public bool InventoryClear(string item_name) 
    {
        /*if (item_name != currentItem)
        {
            for (int i = 0; i <= old_items.Count - 1; i++)
            {
                old_items[i].SetActive(false);
            }
            currentItem = inventory[currentCursor].name;
            return true;
        }
        else {
            return false;
        }*/
        for (int i = 0;i <= allHands.Count - 1;i++)
        {
            allHands[i].GetComponent<SpriteRenderer>().enabled = false;
            foreach (Transform child in allHands[i].transform)
                child.gameObject.SetActive(false);
        }

        /*for (int i = 0; i <= allItems.Count - 1; i++) {
            allItems[i].SetActive(false);
        }*/
        return true;
    }

    public int GetFreeCell() { 
        for (int i = 0;i < inventory.Length;i++)
        {
            if (inventory[i].name == "EmptyPrefab")
            {
                return i;
            }
        }
        return currentCursor;
    }

}
