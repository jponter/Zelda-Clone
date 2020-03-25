using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyLoot : MonoBehaviour
{

    [Header("Loot Stuff")]
    [SerializeField] private LootTable thisLoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeLoot()
    {
        if (thisLoot != null)
        {
            Powerup current = thisLoot.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
