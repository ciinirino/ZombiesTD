using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*compares distance to all towers
 * sets closest tower as target in navmesh
 * 
 * 
 * 
 * 
 * 
 */

public class ZombieScript : MonoBehaviour
{
    public Transform myTarget;
    public UnityEngine.AI.NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //finding and setting navmesh target to closest tower; zombie will auto begin moving untill it has reached the center of the tower's position
        //!! this seems inefficient, calling findobjectsoftype for ever zombie every frame. i dont have a better solution for now.
        myTarget = FindClosestTarget();
        //!!restarts search if no targets are found, this is a placeholder so i dont get spammed with error codes
        if(myTarget == null) { return; }
        agent.SetDestination(myTarget.position);
    }

    //specific for finding objects with towerscripts
    //  checked and behaving properly
    public Transform FindClosestTarget()
    {
        //make a list of all towers in game
        //compare distances
        Transform closestTowerTransform = null;
        float closestTowerDistance = 0f;
        TowerScript[] towersList =  FindObjectsOfType<TowerScript>();
        foreach (TowerScript tower in towersList)
        {


            //set the first tower as the default target to compare to
            if (closestTowerTransform == null)
            {
                closestTowerTransform = tower.transform;
                closestTowerDistance = Vector3.Distance(this.transform.position, tower.transform.position);
            }
            else
            {
                float distance = Vector3.Distance(this.transform.position, tower.transform.position);
                if (distance < closestTowerDistance)
                {
                    closestTowerTransform = tower.transform;
                    closestTowerDistance = Vector3.Distance(this.transform.position, tower.transform.position);
                }
            }


        }
        return closestTowerTransform;
    }

    //used for detetcing contact with towers and destroying them
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            Destroy(other.gameObject);
        }
    }
}
