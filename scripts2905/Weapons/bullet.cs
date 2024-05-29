using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
    
    public bool isBounced = false;
    public int bouceLimit = 5;

    public float startBullet_damage, bullet_damage = 1;
    public float startBullet_speed, bullet_speed;

    public int bounceCount = 0;
    private Vector2 shootVector;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //startBullet_speed = data.bullet_speed;
        //startBullet_damage = data.bullet_damage;
        //startBullet_damage = 0.1f;
        //startBullet_speed = 0.1f;
        bulletLine = GameObject.FindGameObjectWithTag("bulletLine");
        //vector = bulletLine.transform.position;
        shootVector = transform.right * bullet_speed * kf * Time.fixedDeltaTime;
        rb.velocity = shootVector;
    }

    public void PublicStart(GameObject weapon) {
        startBullet_speed = weapon.GetComponentInParent<WeaponStats>().data.bullet_speed;
        startBullet_damage = weapon.GetComponentInParent<WeaponStats>().data.bullet_damage;
        GetComponent<SpriteRenderer>().sprite = weapon.GetComponentInParent<WeaponStats>().data.bulletSprite;
    }

    void FixedUpdate()
    {
        StatsUpdate();
        if (bounceCount == 0)
        {
            shootVector = transform.right * bullet_speed * kf * Time.fixedDeltaTime;
            rb.velocity = shootVector;
        }
        else {
            //shootVector = new Vector2(rb.velocity.x, rb.velocity.y * -1);
            /*Angle a = Vector2.Angle(shootVector, new Vector2(shootVector.x, shootVector.y * -1));
            Angle b = Vector2.Angle(shootVector, new Vector2(shootVector.x * -1, shootVector.y));
            float aFloat;
            float bFloat;
            aFloat = a.ToDegrees(); // по вертикали
            bFloat = b.ToDegrees(); // по горизонтали
            if (aFloat > bFloat)
            {
                shootVector = new Vector2(shootVector.x, shootVector.y * -1);
            }
            else {
                shootVector = new Vector2(shootVector.x * -1, shootVector.y);
            }
            Debug.Log(aFloat);
            Debug.Log(bFloat);
            */
  
        }
        //rb.AddForce(transform.right * bullet_speed * kf * Time.fixedDeltaTime);
    }

    public void Shot(Vector2 direction) {
        //transform.position = Vector2.MoveTowards(transform.position, vector, speed * Time.fixedDeltaTime);
        rb.velocity = direction * 10 * Time.fixedDeltaTime;
        shootVector = transform.right * bullet_speed * kf * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + vector * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag) {
            case "block":
                if (isBounced == false)
                {
                    GameObject.Destroy(prefabObj);
                    //gameObject.SetActive(false);
                }
                else if (isBounced == true) 
                {
                    BounceBullet(collision);
                }
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

    void BounceBullet(Collision2D collision) {
        bounceCount++;
        var firstContact = collision.contacts[0];
        Vector2 newVelocity = Vector2.Reflect(shootVector, firstContact.normal);
        newVelocity = newVelocity.normalized;
        shootVector = newVelocity * bullet_speed * kf * Time.fixedDeltaTime;
        rb.velocity = shootVector;
        //transform.right = transform.right * -1;
        if (bounceCount >= bouceLimit) {
            isBounced = false;
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
