using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutagen : MonoBehaviour
{
    public GameObject Player;
    public float mutagenScaleHP = 1f;
    public float mutagenScaleDamage = 0.01f;
    public float mutagenScaleRate = 0.01f;
    public float mutagenScaleSpeed = 0.01f;

    public int mutagenType = 0;

    public float mutantScale = 0.01f;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        mutagenType = Random.Range(0, 4);
        switch (mutagenType)
        {
            case 0: // ѕя ря
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }
    }


    void MutantScale() {
        Player.GetComponent<Player>().mutantScale += mutantScale;
    }

    public void MutagenHP() {
        Player.GetComponent<Player>().MaxHP += mutagenScaleHP;
        Player.GetComponent<Player>().HP += mutagenScaleHP;
        MutantScale();
    }

    public void MutagenDamage()
    {
        Player.GetComponent<Player>().damageKf += mutagenScaleDamage;
        MutantScale();
    }

    public void MutagenRate()
    {
        Player.GetComponent<Player>().rateKf += mutagenScaleRate;
        MutantScale();
    }

    public void MutagenSpeed()
    {
        Player.GetComponent<Player>().speedKf += mutagenScaleSpeed;
        MutantScale();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
