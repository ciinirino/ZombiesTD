using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * 
 * most likely a temporary object for spawning zombies continously
 * 
 * every time this spawns a zombie, the time between zombie spawns is decreasesed by .1 seconds
 *      this continues untill the spawn time is .5 seconds
 * 
 * currently zombie objectives are attached to the begin round button to become active
 */
public class SpawnerScript : MonoBehaviour
{
    public GameObject zombie;
    //the time between spawns
    public float SpawnTime = 3;
    float lastSpawn = 0;

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.deltaTime;
        if(lastSpawn >= SpawnTime)
        {
            lastSpawn = 0;
            if (SpawnTime > .5) { SpawnTime -= .1f; }
            //!!setting the parent looks messy, but sets the parent to the desired gameobject
            //This object must be the child of the zombies empty gameobject
            Instantiate(zombie, this.transform.position, Quaternion.identity, this.transform.parent.transform);
            
        }
    }
}
