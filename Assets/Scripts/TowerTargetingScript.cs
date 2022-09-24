using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SEPT 23 2022
 * makes a list of all objects with the "Zombie" tag that are within the targeting collider
 * compares the distance to all transforms in the list and finds the closest
 * the transform of that object is collected in the parent tower object
 */
public class TowerTargetingScript : MonoBehaviour
{

    //the targeting collider is exclusively used for finding targets to fire at
    public SphereCollider targetingCollider;

    //a list of all potential targets that enter the target's collider
    List<Transform> targets = new List<Transform>();

    //this currently acts as a firing range as well as a targeting range
    public float targetingRange;

    //read from the parent tower object
    public Transform currentTarget;

    void Start()
    {
        targetingCollider = GetComponent<SphereCollider>();
        targetingCollider.radius = targetingRange;
    }

    void Update()
    {
        currentTarget = ClosestTarget();
    }

    //adding and removing zombies from the targets list as they enter or leave the targeters collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            targets.Add(other.transform);
        }          
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            targets.Remove(other.transform);
        }
    }
 
    public Transform ClosestTarget()
    {
        //if there are no targets in the target list this function will return null
        if (targets.Count == 0)
        {
            return null;
        }

        Transform closestTarget = null;
        float closestDistance = 0;
        
        //compares the distance of each transform in the list of added zombies to this gameobject and returns the transform that is closest
        foreach (Transform t in targets)
        {
            //removing destroyed gameobjects
            if(t == null)
            {
                targets.Remove(t);
                //if removing an object makes the list empty return null
                if(targets.Count == 0)
                {
                    return null;
                }
                //if the list is not empty skip rest of loop for this item
                else { break; }
            }
            
            else
            {
                //setting the first object on the list the default to be compared to
                if (closestTarget == null)
                {
                    closestTarget = t;
                    closestDistance = Vector3.Distance(this.transform.position, t.position);
                }
                else
                {
                    float distance = Vector3.Distance(closestTarget.position, t.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = t;
                    }
                }

            }

        }
        return closestTarget;
    }
}
