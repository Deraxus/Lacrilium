using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string Name;
    [HideInInspector] public float HP = 100;
    public float MaxHP = 100;
    public float speed = 5.0f;
    public int coins = 0;

    public float mutantScale = 0;

    public float damageKf = 1f;
    public float rateKf = 1f;
    public float speedKf = 1f;

    public float startDamageKf;
    public float startRateKf;
    public float startSpeedKf;

    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 vector;

    // Update is called once per frame

    private void Start()
    {
        startDamageKf = damageKf;
        startRateKf = rateKf;
        startSpeedKf = speedKf;
    }
    void Update()
    {
        if (HP <= 0)
        {
            Death();
        }
    }


    public void GetDamage(int damage) {
        HP -= damage;
    }

    public void Death() {
        SceneManager.LoadScene(1);
    }

}
