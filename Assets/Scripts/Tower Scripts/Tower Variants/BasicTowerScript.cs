using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*SUMMARY (Updated oct 26 2022)
 * 
 * this class is for a tower that has a targeting object
 * fires a bulletprefab at the closest target
 * 
 * VARIABLES
 * TowerAttributes
 *      projectile speed
 *      damage
 *      range
 *      attack speed
 * a bullet prefab
 * reference to this tower's targeting object
 * 
 * Functions
 * override Update()
 *      gets a target from targeting object
 *      calculates next attack from attack speed
 *          fires a bullet
 * GetTarget()
 *      reads the current closest target from targeting object
 * CalculateNextAttack()
 *      if atckSpd seconds have passed since last attack
 *          fire a bullet
 * FireBullet()
 *      instantiates a bulletprefab
 *      assigns the bullet's damage variable
 *      pushes with force(projSpd) in direction of target
 */
public class BasicTowerScript : TowerBaseClass
{
    #region Variable Decleration
    [Header("Basic Tower Specific Stats")]
    public TowerAttribute projSpd = new TowerAttribute("Projectile Speed", 0, 0, 0, 0, 0);
    public float projSpdStart;
    public float projSpdInc;

    public TowerAttribute dmg = new TowerAttribute("Damage", 0, 0, 0, 0, 0);
    public float dmgStart;
    public float dmgInc;

    public TowerAttribute range = new TowerAttribute("Range", 0, 0, 0, 0, 0);
    public float rangeStart;
    public float rangeInc;

    public TowerAttribute atckSpd = new TowerAttribute("Attack Speed", 0, 0, 0, 0, 0);
    public float atckSpdStart;
    public float atckSpdInc;

    [Header("Basic Tower Specific Components")]
    public GameObject bulletPrefab;
    public TowerTargetingScript targetingObject;

    private float nextshot;
    private GameObject attackTarget;
    #endregion

    public override void Start()
    {
        base.Start();
        targetingObject = GetComponentInChildren<TowerTargetingScript>();
    }

    public override void InitializeTowerAttibutes()
    {

        base.InitializeTowerAttibutes();

        projSpd.start = projSpdStart;
        projSpd.current = projSpdStart;
        projSpd.upgradeInc = projSpdInc;
        projSpd.upgradeLimit = upgradeLimit;
        attributes.Add(projSpd);

        dmg.start = dmgStart;
        dmg.current = dmgStart;
        dmg.upgradeInc = dmgInc;
        dmg.upgradeLimit = upgradeLimit;
        attributes.Add(dmg);

        range.start = rangeStart;
        range.current = rangeStart;
        range.upgradeInc = rangeInc;
        range.upgradeLimit = upgradeLimit;
        attributes.Add(range);

        atckSpd.start = atckSpdStart;
        atckSpd.current = atckSpdStart;
        atckSpd.upgradeInc = atckSpdInc;
        atckSpd.upgradeLimit = upgradeLimit;
        attributes.Add(atckSpd);
    }

    public override void Update()
    {
        base.Update();

        GetTarget();
        CalculateNextAttack();
    }

    public void GetTarget()
    {
        if(targetingObject.currentTarget != null)
        {
            attackTarget = targetingObject.currentTarget.gameObject;
        }
    }

    public void CalculateNextAttack()
    {
        if (nextshot < atckSpd.current)
        {
            nextshot += Time.deltaTime;
        }
        else
        {
            if(attackTarget != null)
            {
                nextshot = 0f;
                FireBullet();
            }
        }
    }

    public void FireBullet()
    {
        var firedBullet = Instantiate(bulletPrefab, this.transform);
        Vector3 firedDirection = (attackTarget.transform.position - this.transform.position);
        firedBullet.GetComponent<Rigidbody>().AddForce(firedDirection * projSpd.current);
        firedBullet.GetComponent<BulletScript>().bulletDmg = dmg.current;
    }
}
