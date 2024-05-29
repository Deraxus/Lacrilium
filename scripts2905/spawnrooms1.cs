using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnrooms1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int setka_size = 4;
    public GameObject start_room; // ������� ��� ������ 1
    public GameObject start_room2, start_room3; // ������� ��� ������ 2
    public GameObject start_room_cords, start_room_cords2, start_room_cords3;
    private Vector2 vector_top, vector_right;
    private Quaternion rotation;
    public Vector3 pos;
    public GameObject spawned_room;
    public GameObject spawned_room2;
    GameObject PrefabObj;
    GameObject spawn_bot, spawn_left;
    GameObject end_top;
    GameObject end_right;
    public Vector3 endpos_top, endpos_right;
    void Start()
    {
        vector_top = new Vector2(23, 55);
        rotation = new Quaternion(0, 0, 0, 0);
        start_room_cords = start_room;
        start_room_cords2 = start_room2;
        start_room_cords3 = start_room3;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            spawned_room = Instantiate(start_room, vector_top, rotation); // ���������� ������� 1 �� �������� � ������ �����������
            spawn_bot = spawned_room.transform.Find("Grid").Find("spawnblock_bot").gameObject; // �������� ������ �� ��������� � ����� �������
            end_top = spawned_room.transform.Find("Grid").Find("endblock_top").gameObject; // �������� ������ �� ������� � ����� ���������
            end_right = spawned_room.transform.Find("Grid").Find("endblock_right").gameObject; // �������� ������ �� ������� � ����� �������
            endpos_top = end_top.transform.position; // �������� ������� ��������
            endpos_right = end_right.transform.position; // �������� ������� ��������
            endpos_top = new Vector3(endpos_top.x, endpos_top.y + 0.25f, endpos_top.z); // ������� �� 0.25 �����
            vector_top = endpos_top; // ������ ����� ���������� ��� ���������� ������
            vector_right = endpos_right;
            // ������ �������� - ������ �� ��� �������� � ���������� �����������?
            Debug.Log(endpos_top.ToString());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spawned_room2 = Instantiate(start_room2, vector_top, rotation);
            spawn_bot = spawned_room2.transform.Find("Grid").Find("spawnblock_bot").gameObject;
            end_top = spawned_room2.transform.Find("Grid").Find("endblock_top").gameObject;
            endpos_top = end_top.transform.position;
            endpos_top = new Vector3(endpos_top.x, endpos_top.y + 0.25f, endpos_top.z);
            vector_top = endpos_top;
            Debug.Log(endpos_top.ToString());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spawned_room = Instantiate(start_room3, vector_right, rotation); // ���������� ������� 1 �� �������� � ������ �����������
            spawn_bot = spawned_room.transform.Find("Grid").Find("spawnblock_left").gameObject; // �������� ������ �� ��������� � ����� �������
            //end_top = spawned_room.transform.Find("Grid").Find("endblock_top").gameObject; // �������� ������ �� ������� � ����� �������
            end_right = spawned_room.transform.Find("Grid").Find("endblock_right").gameObject; // �������� ������ �� ������� � ����� �������
            //endpos_top = end_top.transform.position; // �������� ������� ��������
            endpos_right = end_right.transform.position; // �������� ������� ��������
            //endpos_top = new Vector3(endpos_top.x, endpos_top.y + 0.25f, endpos_top.z); // ������� �� 0.25 �����
            //vector_top = endpos_top; // ������ ����� ���������� ��� ���������� ������
            vector_right = endpos_right;
            Debug.Log(endpos_top.ToString());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            spawn_left = start_room_cords.transform.Find("Grid").Find("spawnblock_left").gameObject; // �������� ������ �� ���� ������ �����
            Vector2 spawn_left_position = spawn_left.transform.position * setka_size; // �������� ���������� ����� ������ ����� � �������� ���� (� ���������� ����, � ������ ������ �������� ������������ ������ 19)
            vector_right = new Vector2(vector_right.x - spawn_left_position.x * 0.25f, vector_right.y - spawn_left_position.y * 0.25f);
            spawned_room = Instantiate(start_room, vector_right, rotation); // ���������� ������� 1 �� �������� � ������ �����������
            spawn_bot = spawned_room.transform.Find("Grid").Find("spawnblock_left").gameObject; // �������� ������ �� ��������� � ����� �������
            end_top = spawned_room.transform.Find("Grid").Find("endblock_top").gameObject; // �������� ������ �� ������� � ����� �������
            end_right = spawned_room.transform.Find("Grid").Find("endblock_right").gameObject; // �������� ������ �� ������� � ����� �������
            endpos_top = end_top.transform.position; // �������� ������� ��������
            endpos_right = end_right.transform.position; // �������� ������� ��������
            endpos_top = new Vector3(endpos_top.x, endpos_top.y, endpos_top.z); // ������� �� 0.25 �����
            vector_top = endpos_top; // ������ ����� ���������� ��� ���������� ������
            vector_right = endpos_right;
            // ������ �������� - ������ �� ��� �������� � ���������� �����������?
        }
    }
}
