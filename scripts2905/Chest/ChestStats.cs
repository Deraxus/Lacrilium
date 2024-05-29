using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStats : MonoBehaviour
{
    [SerializeField] private SChestStats data;

    [SerializeField] public int rare;

    public bool isOpen = false;

    public List<GameObject> LootCommon = new List<GameObject>();
    public List<GameObject> LootRare = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        rare = data.rare;
        LootCommon = data.LootCommon;
        LootRare = data.LootRare;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
