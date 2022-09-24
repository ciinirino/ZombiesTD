using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * this script handles adding and removing this object from GameController's list of objectives
 * and monitering hp to see when this tower should be destroyed.
 */
public class ZombieObjectiveScript : AttackableObject
{

    // Start is called before the first frame update
    void Start()
    {
        
        GameControllerScript.GC.objectivesList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
            GameControllerScript.GC.objectivesList.Remove(this.gameObject);
        }
    }
}
