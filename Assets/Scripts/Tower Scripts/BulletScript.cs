using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*SUMMARY (Updated Oct 26 2022)
 * this script handles collision with other gameobjects
 *      "Obstacle" tags destroy the bullet
 *      "Zombie" tags destroy the bullet and apply the bullet's damage to the zombie's hp variable
 * this script also handles adding points to the gamecontroller score
 *      !!this should probably be changed to be handled by the zombie so different scores can be added depending on the type of zombie
 *      
 * all data for damage, speed and direction are handled by the tower creating the bullet
 */
public class BulletScript : MonoBehaviour
{
    public float bulletDmg;

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
            GameControllerScript.GC.score += other.GetComponent<ZombieScript>().zombieBounty;
            other.GetComponent<ZombieScript>().UpdateHealth(bulletDmg);
            Destroy(this.gameObject);
            
        }
    }
}
