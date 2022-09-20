using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * !!!raycast to see if zombie is in sight (unable to implement)
 * 
 * Collects a target transform from targeting child object
 * Every intervaldetermined by firingInterval, creates a bullet prefab and applies force in the direction of the collected target
 * 
 */

public class TowerScript : MonoBehaviour
{
    public GameObject Bullet;
    //time between shots
    public float firingInterval = 3f;
    private float nextShot = 0f;

    //the force to be applied to the created bullt
    public float bulletSpeed = 200f;

    //targeting script reference and all data to be passed to it
    public TowerTargetingScript thisTargeting;

    //gets target from child targeting object
    public GameObject currentTarget;



    void Start()
    {
        //create a reference to this towers targeting script and pass it all data required for targeting
        thisTargeting = GetComponentInChildren<TowerTargetingScript>();

    }

    void Update()
    {
        //gets the closest target from the calculations done in the targeting child object
        if (thisTargeting.currentTarget != null)
        {
            currentTarget = thisTargeting.currentTarget.gameObject;

        }
        if (currentTarget != null)
        {
            nextShot += Time.deltaTime;
            if (nextShot >= firingInterval)
            {
                nextShot = 0f;
                FireAtTarget();
            }
        }


    }

    //creates a bullet prefab and applies force on it's rigidbody in the direction of the currentTarget
    public void FireAtTarget()
    {
        var firedBullet = Instantiate(Bullet, this.transform);
        Vector3 firedDirection = (currentTarget.transform.position - this.transform.position);
        firedBullet.GetComponent<Rigidbody>().AddForce(firedDirection * bulletSpeed);
        
    }
    
}
