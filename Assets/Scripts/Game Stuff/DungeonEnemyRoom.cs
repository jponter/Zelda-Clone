using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{

    public Door[] doors;

    public SignalListener enemyUpdate;
    public Vector3 transportPlayer;
    public bool movePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy ) 
            {
                //Debug.Log("enemy active: " + i);
                return; 
            }

        }

        OpenDoors();

    }


    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();

        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open(false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            //if we have a door we might want to move the player
            if (movePlayer)
            {
                other.transform.position += transportPlayer;
            }

            CloseDoors();
            virtualCamera.SetActive(true);
        }

        
    }

    //public override void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && !other.isTrigger)
    //    {
    //        // deactivate all enemies and pots
    //        for (int i = 0; i < enemies.Length; i++)
    //        {
    //            ChangeActivation(enemies[i], false);
    //        }

    //        for (int i = 0; i < pots.Length; i++)
    //        {
    //            ChangeActivation(pots[i], false);
    //        }
    //    }
    //}
}
