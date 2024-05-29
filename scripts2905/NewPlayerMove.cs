using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5.0f;
    Vector2 vector;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<Player>().speed * GetComponent<Player>().speedKf;
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(vector.x) > 0 && Mathf.Abs(vector.y) > 0)
        {
            animator.SetFloat("horizontalmove", 1);
        }
        else {
            animator.SetFloat("horizontalmove", Mathf.Abs(vector.x + vector.y));
        }
    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + vector * speed * Time.fixedDeltaTime);
    }
}
