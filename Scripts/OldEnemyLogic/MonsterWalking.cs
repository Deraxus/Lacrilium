using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterWalking : MonoBehaviour
{
    public float speed = 5;
    public float agrospeed = 10;
    private int mode = 0;
    private float time_start;
    private float goal;
    private Rigidbody2D rb;
    private float flip;
    public GameObject vision;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        flip = GetComponent<Transform>().localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "block")
        {
            flip *= -1;
            Vector3 vector = new Vector3(flip, GetComponent<Transform>().localScale.y, GetComponent<Transform>().localScale.z);
            GetComponent<Transform>().localScale = vector;
        }
    }

    void FixedUpdate()
    {
        time_start += Time.deltaTime;
        if (vision.GetComponent<MonsterVision>().agro == false)
        {
            switch (mode)
            {
                case 0: // ����������� ������� ������� ����� ������ � ���� �������
                    goal = Random.value * 8;
                    //Debug.Log("1231");
                    mode = 1;
                    break;
                case 1: // �������� � ���� �������
                    if (flip > 0)
                    {
                        //Debug.Log("����..., ���� = 1.0");
                        UnityEngine.Vector2 vector = new UnityEngine.Vector2(1, 0);
                        rb.MovePosition(rb.position + vector * speed * Time.fixedDeltaTime);
                        if (time_start > goal)
                        {
                            mode = 2;
                            //Debug.Log("�������, ������ ���� ��������...");
                            time_start = 0;
                        }
                    }
                    else if (flip < 0)
                    {
                        //Debug.Log("����...");
                        UnityEngine.Vector2 vector = new UnityEngine.Vector2(-1, 0);
                        rb.MovePosition(rb.position + vector * speed * Time.fixedDeltaTime);
                        if (time_start > goal)
                        {
                            mode = 2;
                            Debug.Log("�������, ������ ���� ��������...");
                            time_start = 0;
                        }
                    }
                    break;
                case 2: // ����������� ������� �������
                    goal = Random.value * 16;
                    mode = 3;
                    break;
                case 3: // ������ �������
                    if (time_start > goal)
                    {
                        mode = 0;
                        Debug.Log("������� ��������.");
                        time_start = 0;
                    }
                    break;
            }
        }
        else if (vision.GetComponent<MonsterVision>().agro == true) {
            //Debug.Log("� ������");
            UnityEngine.Vector2 vector = player.GetComponent<Transform>().position;
            transform.position = Vector2.MoveTowards(transform.position, vector, speed * Time.fixedDeltaTime);
        }
    }
}
