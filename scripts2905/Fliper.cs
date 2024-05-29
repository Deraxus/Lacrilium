using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    public GameObject player;
    private bool togl = false;
    public GameObject hand1;
    public GameObject hand2;
    public GameObject fliper1;
    public GameObject fliper2;
    public GameObject brother;
    public GameObject camera;
    public bool brother_on;
    private bool togl_mini;
    private float old_scale;
    private float old_scale_minus;
    // Start is called before the first frame update
    void Start()
    {
        if (brother_on == false)
        {
            brother.GetComponent<BoxCollider2D>().enabled = false;
            brother.GetComponent<Fliper_mini>().togl_mini = true;
        }
        old_scale = player.GetComponent<Transform>().localScale.x;
        old_scale_minus = player.GetComponent<Transform>().localScale.x * -1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((togl == true) && (brother.GetComponent<Fliper_mini>().togl_mini == true))
        {
            fliper1.transform.parent = null;
            fliper2.transform.parent = null;
            camera.transform.parent = null;
            player.GetComponent<Transform>().localScale = new Vector3(old_scale_minus, player.GetComponent<Transform>().localScale.y, player.GetComponent<Transform>().localScale.z);
            hand1.GetComponent<PistolRotater>().offset = 180;
            hand2.GetComponent<PistolRotater>().offset = 180;
            fliper1.transform.SetParent(player.transform);
            fliper2.transform.SetParent(player.transform);
            camera.transform.parent = player.transform;
        }
        else if ((togl == false) && (brother.GetComponent<Fliper_mini>().togl_mini == false))
        {
            fliper1.transform.parent = null;
            fliper2.transform.parent = null;
            camera.transform.parent = null;
            player.GetComponent<Transform>().localScale = new Vector3(old_scale, player.GetComponent<Transform>().localScale.y, player.GetComponent<Transform>().localScale.z);
            hand1.GetComponent<PistolRotater>().offset = 0;
            hand2.GetComponent<PistolRotater>().offset = 0;
            fliper1.transform.SetParent(player.transform);
            fliper2.transform.SetParent(player.transform);
            camera.transform.parent = player.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall_WeaponTrue")
        {
            togl = true;
        }
        else if (collision.gameObject.name == "Wall_WeaponFalse")
        {
            togl = false;
        }
    }

}
