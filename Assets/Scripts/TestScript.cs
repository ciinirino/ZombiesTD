using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    /* PHASE ONE GOALS - GAMEOBJECT BASICS
     *  $make zombies move towards towers (pathfinding) $
     *  $create bullets $
     *  $make towers fire at closest zombie in range $
     *     $ make bullets be destroyed if colliding with obstacle $
     *      $make bullets be destroyed if leaving game area $ (obstacles around game area
     *      $make bullets kill zombies $
     * $ make zombies kill towers $
     *  
     *  
     * PROBLEMS
     * 
     * %%%
     *  RAYCASTING FROM TOWER TO TARGET ZOMBIE
     *      bug: raycast detects obstacles, but not the zombie
     * 
    *
     * raycast is pointed in right direction
     * ray hits wall and since the tag doesnt match, returns notseen
     * when zombie comes around the wall the ray returns notarget
     *      so the ray is not recognizing or not colliding with the zombie

    public void CheckSightLine()
    {

        Debug.Log(Physics.Raycast(transform.position, currentTarget.transform.position, out hit));
        Physics.Raycast(transform.position, currentTarget.transform.position, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                Debug.Log("cansee");
            }
            else if (hit.collider.gameObject.tag != "Zombie")
            {
                Debug.Log("cantsee");

            }
        }
        else if (hit.collider == null)
        {
            Debug.Log("notarget");
        }


    }
     * %%%
     * 
     * EASIER CREATION OF LEVELS IN EDITOR
     * harder / more time consuming to implement than i thought
     * https://docs.unity3d.com/ScriptReference/MenuItem.html
     * https://answers.unity.com/questions/381630/listen-for-a-key-in-edit-mode.html
     * 
     * %%%
     * 
     * BARRICADES AND NAVMESH
     * currently my solution to not having barricades be partof the navmesh is to bake the scene then add the barricades.
     * this might work long term as objects created play are not par of the navmesh either
     * 
     * %%%
     * 
     * PHASE TWO GOALS - UI
     * 
     * $make a score for killing zombies
     * $make clickable tiles for building towers
     * $make purchasing towers cost points
     * $make zombies spawn continuosly
     * $make zombie impassible walls but can shoot through
     * $add barricades
     * !!add generating boarders and floors
     * !!add generating buildable spaces
     * $make buildible spaces return after tower / wall is destroyed
     * $add begin round button
     * $add moving camera 
     * $add zombie objective
     * $add life for zombie objective
     * $hp can be adjusted in inspector, remove the individual script's hp assigning
     * $add tower life
     * 
     * PHASE 2.5 CREATING LEVELS IN SCENE
     * 
     * 
     * PHASE 3 ZOMBIE AND TOWER INTELLIGENCE
     * 
     * add being unable to build something that has a zombie in the space
     * add calculating if zombie can reach tower / objectie, if not remove from list
     * add zombie spawning limit
     * add making closest target for zombies and towers be based on pathfinding
     * add selling towers
     * add tower / objective / barricade hp bar (makea function that updates the hpbar in attackable clasee, call in inheriting class's update)
     * add camera bounds
     * clean up UI
     * 
     * 
     * PHASE 4 UPGRADING TOWERS & SPECIAL ZOMBIES
     * 
     * 
     *  
     */
}