using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public List<GameObject> LootCommon = new List<GameObject>();
    public List<GameObject> LootRare = new List<GameObject>();
    public GameObject coin;

    public int minCoinCount = 1;
    public int maxCoinCount = 20;

    public int itemChance = 20;
    public float forceUp = 400f;
    public float forceLR = 100f;
    public float lootDelay = 2;
    public float fallDelay = 2;

    private int LootNumber;
    GameObject publicLoot;
    void Start()
    {
        
    }

    public void LootDrop()
    {
        Debug.Log("ОТКРЫТИЕ СУНДУКА");
        int randNumber = Random.Range(0, 100);
        if ((randNumber <= (itemChance))) {
 
            StartCoroutine(LootDropCor(mode : 0));
        }
        else
        {
            randNumber = Random.Range(minCoinCount, maxCoinCount);
            StartCoroutine(LootDropCor(mode : 1, coinDropCount : randNumber));
        }
    }

    /*void SpamLoot(int lootCount, float localLootDelay) {
        for (int i = 0; i < lootCount; i++)
        {
            if (i == 0)
            {
                StartCoroutine(SpamLootCor(lootDelay));
            }
            else {
                StartCoroutine(SpamLootCor(localLootDelay));
            }

        }
    }
    IEnumerator SpamLootCor(float localLootDelay) {
        yield return new WaitForSeconds(localLootDelay);
        StartCoroutine(LootDropCor(1));

    }*/

    public IEnumerator LootDropCor(int mode = 0, int coinDropCount = 5,int localCount = 0, Vector2 whereSpawn = default){  // mode = 0 - обычный режим, 1 - монетки из сундука, 2 - мутагены, 3 - мугатены + лут
        List<GameObject> localCommonLootList = new List<GameObject>();
        List<GameObject> localRareLootList = new List<GameObject>();
        foreach (GameObject obj in LootCommon) {
            bool togl = true;
            foreach (GameObject localLoot in GameObject.Find("Player").GetComponent<MainInventory>().allItems) { 
                if ((obj.name.Equals(localLoot.name))){
                    togl = false;
                }
            }
            if (togl) { 
                localCommonLootList.Add(obj);
            }
        }

        foreach (GameObject obj in LootRare)
        {
            bool togl = true;
            foreach (GameObject localLoot in GameObject.Find("Player").GetComponent<MainInventory>().allItems)
            {
                if ((obj.name.Equals(localLoot.name))){
                    togl = false;
                }
            }
            if (togl)
            {
                localRareLootList.Add(obj);
            }
        }

            /*foreach (GameObject obj in LootRare)
            {
                if (GameObject.Find("Player").GetComponent<MainInventory>().allItems.Contains(obj) == false)
                {
                    localRareLootList.Add(obj);
                }
            }*/
        if (mode == 0)
        {
            Debug.Log("Выпал предмет");
        }
        else {
            Debug.Log("Выпала монетка");
        }
        float coinLootDelay;
        GameObject loot = coin;
        coinLootDelay = Mathf.Pow(coinDropCount * 3, -1);
        if ((mode == 1) && (localCount > 0))
        {
            yield return new WaitForSeconds(coinLootDelay);
        }
        else {
            Debug.Log("ТУТА" + localCount);
            yield return new WaitForSeconds(lootDelay);
        }
        if (mode == 0)
        {
            LootNumber = Random.Range(0, 101);
            if (LootNumber < 80)
            {
                loot = localCommonLootList[Random.Range(0, localCommonLootList.Count)];
                Debug.Log(loot.name);
            }
            else
            {
                loot = localRareLootList[Random.Range(0, localRareLootList.Count)];
                Debug.Log(loot.name);
            }
        }
        if (loot.name == "EmptyPrefab") {
            yield break;
        }
        else if (mode == 1)
        {
            loot = coin;
        }
        Vector3 vector;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        if (whereSpawn == default)
        {
            vector = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
            Debug.Log(vector + "абв");
        }
        else
        {
            vector = new Vector3(whereSpawn.x, whereSpawn.y, 20);
            Debug.Log(vector + "абв");
        }
        loot = Instantiate(loot, vector, rotation);
        Debug.Log("заспаунил");
        float randForce = Random.Range(forceUp - (forceUp / 4), (forceUp + forceUp) / 4);
        float randDelayUp = randForce / 220;
        float randDelayLR = randForce / 4 - forceUp / 5;
        fallDelay = randDelayUp;
        loot.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * randForce * 0.02f, ForceMode2D.Impulse);
        if (Random.Range(0, 2) == 1) {
            loot.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.left * randDelayLR * 0.02f, ForceMode2D.Impulse);
        }
        else
        {
            loot.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.right * randDelayLR * 0.02f, ForceMode2D.Impulse);
        }
        StartCoroutine(StopForce(loot));
        if ((mode == 1) && (localCount < coinDropCount)) {;
            Debug.Log("Ага");
            StartCoroutine(LootDropCor(mode: 1, localCount : localCount + 1, coinDropCount : coinDropCount));
        }
    }

    IEnumerator StopForce(GameObject loot)
    {
        yield return new WaitForSeconds(fallDelay);
        loot.GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        loot.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }
}
