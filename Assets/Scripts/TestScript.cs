using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    /* PHASE ONE GOALS
     *  $make zombies move towards towers (pathfinding) $
     *  $create bullets $
     *  $make towers fire at closest zombie in range $
     *     $ make bullets be destroyed if colliding with obstacle $
     *      $make bullets be destroyed if leaving game area $ (obstacles around game area
     *      $make bullets kill zombies $
     *  make zombies kill towers 
     *  
     *  
     * PROBLEMS
     * 
     * %%%
     *  making a raycast to see if zombie is in sightline of tower
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
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */
}