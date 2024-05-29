using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wherespawn;
    public GameObject bullet;
    public float timer;

    public Sprite bullet_sprite;

    private Vector2 position;

    private float startRate, rate;
    public int startPatrons, patrons;
    public float startReload_time, reload_time;

    private GameObject Stats;

    private int old_patrons;

    private bool tog = true;
    void Start()
    {
        /*Stats = GameObject.Find("Stats");
        rate = GameObject.Find("Stats").GetComponent<WeaponStats>().rate;
        patrons = GameObject.Find("Stats").GetComponent<WeaponStats>().patrons;
        reload_time = GameObject.Find("Stats").GetComponent<WeaponStats>().reload_time;
        old_patrons = GameObject.Find("Stats").GetComponent<WeaponStats>().patrons;
        Debug.Log("—“¿–“");*/
        startRate = GetComponentInParent<WeaponStats>().rate;
        patrons = GetComponentInParent<WeaponStats>().patrons;
        startReload_time = GetComponentInParent<WeaponStats>().reload_time;
        old_patrons = GetComponentInParent<WeaponStats>().patrons;
    }

    // Update is called once per frame
    void Update()
    {
        StatsUpdate();
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer >= Mathf.Pow(rate, -1) && patrons > 0)
        {
            timer = 0f;
            Shot();
        }
        if ((patrons <= 0 && tog == true) || (Input.GetKey("r") && tog == true))
        {
            tog = false;
            StartCoroutine(Reload());
        }

    }

    void Shot()
    {
        patrons -= 1;
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //Debug.Log("—Ô‡‚Ì");
        //Instantiate(bullet, wherespawn.transform.position, Quaternion.Euler(0f, 0f, rotZ)).GetComponent<bullet>().enabled = true;
        //GameObject spawnedBullet = Instantiate(bullet, wherespawn.transform.position, Quaternion.Euler(0f, 0f, rotZ));
        GameObject spawnedBullet = Instantiate(bullet, wherespawn.transform.position, transform.rotation);
        spawnedBullet.GetComponentInChildren<bullet>().direction = position;
        spawnedBullet.GetComponentInChildren<bullet>().PublicStart(gameObject);
        Debug.Log(2);
    }

    public void RateUp(float kf) {
        rate = rate * kf;
    }

    public void RateDown(float kf) {
        rate = rate / kf;
    }

    void StatsUpdate() {
        GameObject player = GameObject.Find("Player");
        rate = startRate * player.GetComponent<Player>().rateKf;
        reload_time = startReload_time;
    }

    IEnumerator Reload()
    {
        patrons = 0;
        GameObject.Find("Player").gameObject.GetComponent<MainInventory>().canSwap = false;
        yield return new WaitForSecondsRealtime(reload_time);
        patrons = old_patrons;
        tog = true;
        GameObject.Find("Player").gameObject.GetComponent<MainInventory>().canSwap = true;
    }
}
