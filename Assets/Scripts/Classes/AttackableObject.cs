using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * 
 * this has an HP variable set in inpector, and a function for modifying hp based on the dmg called from the attacking object
 *      also moniters HP and destroys the object when it reaches 0
 */
public class AttackableObject : MonoBehaviour
{
    //the health of the object, set in inspector
    public int hp;

    public void UpdateHealth(int dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
