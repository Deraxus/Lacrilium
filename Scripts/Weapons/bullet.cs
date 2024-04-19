using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bulletLine;
    public GameObject weapon;
    private Vector2 vector;
    public GameObject wherespawn;
    public float kf = 1;

    public GameObject prefabObj;
    public Sprite bulletSprite;

    public SWeaponStats data;

    public Vector2 direction;

    public float startBullet_damage, bullet_damage = 1;
    public float startBullet_speed, bullet_speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //startBullet_speed = data.bullet_speed;
        //startBullet_damage = data.bullet_damage;
        //startBullet_damage = 0.1f;
        //startBullet_speed = 0.1f;
        bulletLine = GameObject.FindGameObjectWithTag("bulletLine");
        //vector = bulletLine.transform.position;
    }

    public void PublicStart(GameObject weapon) {
        startBullet_speed = weapon.GetComponentInParent<WeaponStats>().data.bullet_speed;
        startBullet_damage = weapon.GetComponentInParent<WeaponStats>().data.bullet_damage;
        GetComponent<SpriteRenderer>().sprite = weapon.GetComponentInParent<WeaponStats>().data.bulletSprite;
    }

    void FixedUpdate()
    {
        StatsUpdate();
        rb.velocity = transform.right * bullet_speed * kf * Time.fixedDeltaTime;
        //rb.AddForce(transform.right * bullet_speed * kf * Time.fixedDeltaTime);
    }

    public void Shot(Vector2 direction) {
        //transform.position = Vector2.MoveTowards(transform.position, vector, speed * Time.fixedDeltaTime);
        rb.velocity = direction * 10 * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + vector * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag) {
            case "block":
                GameObject.Destroy(prefabObj);
                //gameObject.SetActive(false);
                break;
            case "chest":
                GameObject.Destroy(prefabObj); break;
            case "Enemy":
                DamageEnemy(collision.gameObject, bullet_damage);
                break;
            case "box":
                DamageBox(collision.gameObject, bullet_damage);
                break;
        }
    }

    void StatsUpdate() {
        GameObject player = GameObject.Find("Player");
        bullet_damage = startBullet_damage * player.GetComponent<Player>().damageKf;
        bullet_speed = startBullet_speed;
        
    }
    private void DamageEnemy(GameObject enemy, float damage) {
        enemy.gameObject.GetComponent<EnemyLogic>().HP -= damage;
        GameObject.Destroy(prefabObj);
    }
    private void DamageBox(GameObject box, float damage)
    {
        box.gameObject.GetComponent<BoxLogic>().HP -= damage;
        GameObject.Destroy(prefabObj);
    }
}
