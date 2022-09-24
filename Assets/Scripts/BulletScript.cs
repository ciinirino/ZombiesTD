using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * 
 * this script handles collision with other gameobjects
 *      "Obstacle" tags destroy the bullet"
 *      "Zombie" tags destroy both the bullet and the zombie
 * this script also handles adding points to the gamecontroller score
 *      !!this should probably be changed to be handled by the zombie so different scores can be added depending on the type of zombie
 *      
 * all data for speed and direction are handled by the tower creating the bullet
 */
public class BulletScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //if the hit object is a wall destroy the bullet
        if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
        //if the object hit a zombie destroy both the bullet and the zombie
        else if (other.CompareTag("Zombie"))
        {
            GameControllerScript.GC.score++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
