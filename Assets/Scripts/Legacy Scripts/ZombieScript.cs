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
 * SEPT 26 2022
 * checks to see if the zombie can path to chosen move target
 *      if it cannot and the target is a tower, remove it from the global list of objectives in BoardController
 * 
 */

public class ZombieScript : ZombieInfo
{
    //gameobject components
    UnityEngine.AI.NavMeshAgent agent;

    //where the zombie is moving to
    Transform zombieMoveTarget;
    //who the zombie is attacking
    GameObject zombieAttackTarget;


    //used in handling attackspeed
    float lastAttack;

    //changing the color of zombie based on its hp
    public Material myMat;
    public Color baseColor;
    public float hpPercent;


    // Start is called before the first frame update
    void Start()
    {
        //fetching gameobject components
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //speed is set in inspector and passed to navmeshAgent
        agent.speed = zombieSpeed;

        myMat = GetComponent<Renderer>().material;
        baseColor = myMat.color;

        HP = startHP;
    }

    // Update is called once per frame
    void Update()
    {
        //finding and setting navmesh target to closest tower; zombie will auto begin moving untill it has reached the center of the tower's position
        zombieMoveTarget = FindClosestTarget();
        //!!restarts search if no targets are found, this is a placeholder so i dont get spammed with error codes
        if(zombieMoveTarget == null)
        { 
            return; 
        }

        agent.SetDestination(zombieMoveTarget.position);

        //checks to see if it can path to the current target, if it cannot it removes the tower from the global list
        //currently outdated / unusable
        /*
        if(agent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            if (zombieMoveTarget.CompareTag("Tower"))
            {
                zombieMoveTarget.GetComponent<TowerBaseClass>().RemoveFromGCObjectivesList();
                agent.ResetPath();
                return;
            }
            else if (zombieMoveTarget.CompareTag("ZombieObjective"))
            {
                Debug.Log("Zombie cannot find path to objective");
            }
        }
        */
        //if the zombie is attacking it cannot be moving
        if (zombieAttackTarget == null)
        {
            agent.speed = zombieSpeed;
        }
        else
        {
            agent.speed = 0;
        }

        //zombies attack by directly modifying the hp of the target object in intervals of attackspeed
        if (lastAttack < zombieAttackSpeed)
        {
            lastAttack += Time.deltaTime;
        }
        else if (lastAttack >= zombieAttackSpeed)
        {
            if (zombieAttackTarget != null)
            {

                //all objects that the zombie can attack shouldinherit from the attackableObject class
                zombieAttackTarget.GetComponent<TowerBaseClass>().UpdateHp(zombieDmg);

                lastAttack = 0;
            }
        }

        //changing the color of the zombie based on its hp
        hpPercent = HP / startHP;
        if(hpPercent != 1)
        {
            myMat.color = baseColor - new Color(hpPercent, 0f, 0f);
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
