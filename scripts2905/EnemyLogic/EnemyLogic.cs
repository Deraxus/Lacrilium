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
    public float damage;

    public float speed;
    private int steps_limit;
    private int koef = 10;
    public int physKoef = 10;
    public bool IsDead = false;

    public GameObject damageCloud;

    private GameObject player;
    public int State = 0;
    public bool IsBlock = false;
    public bool isAttack = false;
    public bool isMoving = true;


    /*���������: 0 - ��������������
    1 - ����� �� ���������
    */

    public int steps = 0;

    private GameObject Player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        HP = data.HP;
        damage = data.Damage;
        speed = data.speed;
        steps_limit = data.steps_limit;
    }

    private void Start()
    {
        HP = Random.Range((HP - (HP / koef)), (HP + (HP / koef)));
        speed = Random.Range((speed - (speed / koef)), (speed + (speed / koef)));
        Player = GameObject.Find("Player");
    }

    int direction = 0;
    int old_direction = 0;
    int rand_stay = 0;
    Vector2 vector;
    // Update is called once per frame
    void FixedUpdate()
    {
        float playerX = Player.transform.position.x;
        float playerY = Player.transform.position.y;
        if ((HP <= 0) && (IsDead == false))
        {
            EnemyDeath();
        }
        if (isMoving == true) {
            EnemyMove();
        }

        if (playerX < transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            steps = steps_limit / 2 - 5;
        }
        if (collision.gameObject.tag == "player1") {
            DamageEntity(collision.gameObject, damage, null);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player1")
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
        GameObject.Find("EventMng").GetComponent<EventMng>().OnEnemyDiedEvent(obj);
    }

    IEnumerator FullEnemyDeath(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    public IEnumerator DamageEntity(GameObject damageCloud, float damage, GameObject pos) {
        gameObject.GetComponent<Animator>().SetTrigger("IsAttacking");
        isMoving = false;
        isAttack = true;
        GetComponent<AIPath>().canMove = false;
        yield return new WaitForSeconds(0.5f);
        Quaternion smth = new Quaternion(0, 0, 0, 0);
        Vector3 position = pos.transform.position;
        Instantiate(damageCloud, position, smth).GetComponentInChildren<DamageCloudLogic>().damage = damage;
        yield return new WaitForSeconds(0.35f);
        isAttack = false;
        isMoving = true;
        GetComponent<AIPath>().canMove = true;
    }

    void EnemyMove() {
        switch (State)
        {
            case 0:
                GetComponent<AIPath>().canMove = false;
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
                //vector = player.transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, vector, speed * Time.fixedDeltaTime);
                //Debug.Log(vector);
                //transform.Translate(vector * speed * Time.fixedDeltaTime);
                //rb.AddForce(vector * speed * Time.fixedDeltaTime * 10000, ForceMode2D.Force);
                //Debug.Log(player.transform.position);
                //rb.MovePosition(vector * speed * Time.fixedDeltaTime);
                GetComponent<AIPath>().canMove = true;
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
