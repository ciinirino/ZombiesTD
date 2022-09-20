using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  finds tranform of closest zombie in range
 *  this is done by using a collider to detect trigger entry, and if the tag of the trigger is a zombie adding it to a list of targets
 *  then comparing the distance of this object to each item on the list and returning the tranform with the smallest distance
 *  the transform of that object is collected in the parent tower object
 * 
 * 
 */
public class TowerTargetingScript : MonoBehaviour
{

    //the targeting collider is exclusively used for finding targets
    public SphereCollider targetingCollider;

    //a list of all potential targets that enter the target's collider
    public List<Transform> targets = new List<Transform>();

    //this acts as a firing range as well as a targeting range
    public float targetingRange;

    //collected in the parent tower object
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
                //if this makes the list empty return null
                if(targets.Count == 0)
                {
                    return null;
                }
                //if the list is not empty skip rest of loop for this item
                else { break; }
            }
            
            else
            {
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
