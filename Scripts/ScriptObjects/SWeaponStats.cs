using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class SWeaponStats : ScriptableObject
{
    [SerializeField] public float bullet_damage = 5;
    [SerializeField] public int patrons = 10;

    [SerializeField] public Sprite bulletSprite;

    [SerializeField] public float reload_time = 3.0f;
    [SerializeField] public float rate = 5.0f; // Выстрелов в секунду
    [SerializeField] public float bullet_speed = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
