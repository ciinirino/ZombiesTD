using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * SEPT 23 2022
 * compares the distance of this object to all objects in gamecontroller's objectivesList
 *      this is updated every frame; if a new closer object appears or the current target is destroyed, the zombie will change targets
 * sets the navmeshAgent destination to the closest object in the list
 *      if there are no objectives, the zombie will wait and do nothing untill one appears
 * detects collision with objects of specific tags, and goes to attack mode if in contact
 *      while attacking the zombie will not move, and deal attackDamage every attackSpeed
 * 
 */

public class ZombieScript : MonoBehaviour
{
    //gameobject components
    UnityEngine.AI.NavMeshAgent agent;

    //where the zombie is moving to
    Transform zombieMoveTarget;
    //who the zombie is attacking
    GameObject zombieAttackTarget;

    [Header("Zombie Attributes")]
    public int moveSpeed = 2;
    public float attackSpeed = 2;
    public int attackDamage = 1;

    //used in handling attackspeed
    float lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        //fetching gameobject components
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //speed is set in inspector and passed to navmeshAgent
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //finding and setting navmesh target to closest tower; zombie will auto begin moving untill it has reached the center of the tower's position
        zombieMoveTarget = FindClosestTarget();
        //!!restarts search if no targets are found, this is a placeholder so i dont get spammed with error codes
        if(zombieMoveTarget == null) { return; }

        agent.SetDestination(zombieMoveTarget.position);
        
        //if the zombie is attacking it cannot be moving
        if (zombieAttackTarget == null)
        {
            agent.speed = moveSpeed;
        }
        else
        {
            agent.speed = 0;
        }

        //zombies attack by directly modifying the hp of the target object in intervals of attackspeed
        if (lastAttack < attackSpeed)
        {
            lastAttack += Time.deltaTime;
        }
        else if (lastAttack >= attackSpeed)
        {
            if (zombieAttackTarget != null)
            {

                //all objects that the zombie can attack shouldinherit from the attackableObject class
                zombieAttackTarget.GetComponent<AttackableObject>().UpdateHealth(attackDamage);

                lastAttack = 0;
            }
        }   
    }

    //specific for finding distance to objects in gamecontroller's list of objectives
    public Transform FindClosestTarget()
    {
        //currently compares the raw distance from this zombies transform to the transform of every object in gamecontroller's objectives
        //!!would like to change this to compare pathfinding distances
        Transform closestTowerTransform = null;
        float closestTowerDistance = 0f;
        foreach (GameObject objective in GameControllerScript.GC.objectivesList)
        {
            //set the first tower as the default target to compare to
            if (closestTowerTransform == null)
            {
                closestTowerTransform = objective.transform;
                closestTowerDistance = Vector3.Distance(this.transform.position, objective.transform.position);
            }
            else
            {
                float distance = Vector3.Distance(this.transform.position, objective.transform.position);
                if (distance < closestTowerDistance)
                {
                    closestTowerTransform = objective.transform;
                    closestTowerDistance = Vector3.Distance(this.transform.position, objective.transform.position);
                }
            }
        }
        return closestTowerTransform;
    }

    //used for detecting contact with attackables and destroying them
    //!!maybe instead of checking tags, check to see if they have the component attackable?
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barricade") || other.CompareTag("Tower") || other.CompareTag("ZombieObjective"))
        {
            zombieAttackTarget = other.gameObject;
        }
    }
}
