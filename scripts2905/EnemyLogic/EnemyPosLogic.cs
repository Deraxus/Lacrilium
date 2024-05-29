using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Player") && (GetComponentInParent<EnemyLogic>().isAttack == false)) {
            StartCoroutine(GetComponentInParent<EnemyLogic>().DamageEntity(GetComponentInParent<EnemyLogic>().damageCloud, GetComponentInParent<EnemyLogic>().damage, gameObject));
        }
    }
}
