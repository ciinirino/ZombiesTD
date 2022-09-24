using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * !!raycast to see if zombie is in sight (unable to implement, detailed in TestScript)
 * 
 * Collects a target transform from targeting child object
 * Every firingSpeed seconds, creates a bullet prefab and applies force in the direction of the collected target
 * health is handled by AttackableObject class
 *      monitering health and when to destroy the tower is handled in the updateHealth() function in AttackableObject, which is called by the attacking zombie
 * parent is handled by BuildableAndAttackableObject class
 * when created this script adds it's gameobject to the global objectivesList in GameController, but is removed in GameController when destroyed
 */

public class TowerScript : BuildableAndAttackableObject
{
    [Header("Tower Attributes")]
    //time between shots
    public float firingSpeed = 3f;
    private float nextShot = 0f;
    //bullet prefab to be created
    public GameObject Bullet;
    //the force to be applied to the created bullt
    public float bulletSpeed = 200f;

    //this towers' targeting object targetingScript, used to read the result of ClosestTarget()
    public TowerTargetingScript thisTargeting;

    //gets target from child targeting object
    public GameObject currentTarget;

    private void Awake()
    {
        //add tower to global list of objectives
        GameControllerScript.GC.objectivesList.Add(this.gameObject);
    }

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
        //this if statement is set up in such a way that the tower readies its next shot even if no zombie is in range
        if (nextShot >= firingSpeed)
        {
            if(currentTarget != null)
            {
                nextShot = 0f;
                FireAtTarget();
            }
        }
        else
        {
            nextShot += Time.deltaTime;
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
