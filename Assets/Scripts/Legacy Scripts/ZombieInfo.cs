using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * OCT 17 2022
 * 
 * this is a class used to store info aboout zombies
 * 
 * 
 * OCT 18 2022
 * 
 * change this class to ZombieBaseClass
 */
public class ZombieInfo : MonoBehaviour
{
    [Header("Attributes To Be Set In Inspector")]
    public string zombieName;
    public float zombieSpeed;
    public float startHP;
    public float HP;
    public float zombieDmg;
    public float zombieAttackSpeed;
    public int zombieBounty;


    public void UpdateHealth(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
