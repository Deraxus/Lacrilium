using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    [SerializeField] private SEnemyStats data;

    public float HP;
    private float Damage;
    public float speed;
    private int steps_limit;
    private int koef = 10;
    public int physKoef = 10;
    public bool IsDead = false;
    private AIPath path;

    private GameObject player;
    public int State = 0;
    public bool IsBlock = false;
    public bool isMoving = true;
    /*���������: 0 - ��������������
    1 - ����� �� ���������
    */

    public int steps = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        HP = data.HP;
        Damage = data.Damage;
        speed = data.speed;
        steps_limit = data.steps_limit;
    }

    private void Start()
    {
        HP = Random.Range((HP - (HP / koef)), (HP + (HP / koef)));
        speed = Random.Range((speed - (speed / koef)), (speed + (speed / koef)));
        path = GetComponent<AIPath>();
        path.maxSpeed = speed / 2;
    }

    int direction = 0;
    int old_direction = 0;
    int rand_stay = 0;
    Vector2 vector;
    // Update is called once per frame
    void FixedUpdate()
    {
        if ((HP <= 0) && (IsDead == false))
        {
            EnemyDeath();
        }
        if (isMoving == true) {
            GetComponent<AIPath>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = true;
            EnemyMove();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            steps = steps_limit / 2 - 5;
        }
        if (collision.gameObject.tag == "player") {
            DamageEntity(collision.gameObject, Damage);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            isMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name == "FullRoom")
            {
                collision.GetComponentInParent<RoomStats>().EnemyCount += 1;
            }
        }
        catch
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name == "FullRoom")
            {
                //collision.gameObject.transform.parent.gameObject.transform.Find("Stats").GetComponent<RoomStats>().EnemyCount -= 1;
                collision.GetComponentInParent<RoomStats>().EnemyCount -= 1;
            }
        }
        catch {
            
        }
    }

    void EnemyDeath() {
        IsDead = true;
        Transform pos = transform;
        int randInt = Random.Range(GetComponent<Loot>().minCoinCount, GetComponent<Loot>().maxCoinCount);
        int randInt2 = Random.Range(0, 101);
        if ((randInt2 <= (GetComponent<Loot>().itemChance * 100)))
        {
            StartCoroutine(GetComponent<Loot>().LootDropCor(mode: 0, whereSpawn: pos.position, coinDropCount: randInt));
        }
        else
        {
            StartCoroutine(GetComponent<Loot>().LootDropCor(mode: 1, whereSpawn: pos.position, coinDropCount: randInt));
        }
        DisableFull(gameObject);
        StartCoroutine(FullEnemyDeath(5.2f));
    }

    void DisableFull(GameObject obj) {
        obj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        obj.GetComponent<EnemyLogic>().enabled = false;
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<Rigidbody2D>().simulated = false;
    }

    IEnumerator FullEnemyDeath(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    void DamageEntity(GameObject entity, float damage) {
        isMoving = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        if (entity.tag == "player") {
            entity.GetComponent<PlayerStats>().HP -= damage;
        }
    }

    void EnemyMove() {
        switch (State)
        {
            case 0:
                path.enabled = false;
                if (steps == rand_stay || IsBlock == true)
                {
                    vector = new Vector2(0, 0);
                    old_direction = direction;
                    IsBlock = false;
                }
                if (steps == 0)
                {
                    direction = Random.Range(0, 8);
                    while (direction == old_direction || direction > old_direction - 2 && direction < old_direction + 2)
                    {
                        direction = Random.Range(0, 8);
                    }
                    switch (direction)
                    {
                        case 0:
                            vector = new Vector2(0, 1);
                            break;
                        case 1:
                            vector = new Vector2(1, 1);
                            vector.Normalize();
                            break;
                        case 2:
                            vector = new Vector2(1, 0);
                            break;
                        case 3:
                            vector = new Vector2(1, -1);
                            vector.Normalize();
                            break;
                        case 4:
                            vector = new Vector2(0, -1);
                            break;
                        case 5:
                            vector = new Vector2(-1, -1);
                            vector.Normalize();
                            break;
                        case 6:
                            vector = new Vector2(-1, 0);
                            break;
                        case 7:
                            vector = new Vector2(-1, 1);
                            vector.Normalize();
                            break;
                    }
                }
                rb.AddForce(vector * speed * physKoef * Time.fixedDeltaTime);
                break;
            case 1:
                path.enabled = true;
                //vector = player.transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, vector, speed * Time.fixedDeltaTime);
                //Debug.Log(vector);
                //transform.Translate(vector * speed * Time.fixedDeltaTime);
                //rb.AddForce(vector * speed * Time.fixedDeltaTime * 10000, ForceMode2D.Force);
                //Debug.Log(player.transform.position);
                //rb.MovePosition(vector * speed * Time.fixedDeltaTime);
                break;

        }
        steps++;
        if (steps == steps_limit * 2)
        {
            steps = 0;
            rand_stay = Random.Range(steps_limit, steps_limit * 8);
        }
    }
}
