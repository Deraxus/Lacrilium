using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    // Start is called before the first frame update
    int freeCell;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            TakeCoin(collision.gameObject);
        }
        else if (collision.gameObject.tag == "mutagen")
        {
            TakeMutagen(collision.gameObject);
        }
        else if (collision.gameObject.tag == "item") {
            if (GetComponent<MainInventory>().canSwap == true || GetComponent<MainInventory>().canSwap == false)
            {
                TakeItemF(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "poison")
        {
            TakePoison(collision.gameObject);
        }
    }

    void TakeCoin(GameObject coin) {
        GetComponent<Player>().coins++;
        Destroy(coin);
    }

    void TakeMutagen(GameObject mutagen) {
        mutagen.GetComponent<Mutagen>().Player = gameObject;
        switch (mutagen.GetComponent<Mutagen>().mutagenType)
        {
            case 0:
                mutagen.GetComponent<Mutagen>().MutagenHP();
                break;
            case 1:
                mutagen.GetComponent<Mutagen>().MutagenDamage();
                break;
            case 2:
                mutagen.GetComponent<Mutagen>().MutagenRate();
                break;
            case 3:
                mutagen.GetComponent<Mutagen>().MutagenSpeed();
                break;
        }
        Destroy(mutagen);
    }
    void TakePoison(GameObject poison)
    {
        if (poison.name == "damagePoison_sprite") {
            FakeDestroying(poison);
            poison.GetComponent<DamagePoison>().OnTaking();
        }
    }
    void FakeDestroying(GameObject obj)
    {
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<SpriteRenderer>().enabled = false;
        foreach (var component in obj.GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }
    }

    void TakeItemF(GameObject item) {
        Debug.Log("Взятие");
        freeCell = gameObject.GetComponent<MainInventory>().GetFreeCell();
        gameObject.GetComponent<MainInventory>().inventory[freeCell] = item.gameObject.GetComponentInChildren<LinkLootSprites>().linkSprite.gameObject;
        Destroy(item);
    }
}
