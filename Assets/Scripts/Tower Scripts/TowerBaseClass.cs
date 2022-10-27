using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* SUMMARY (Updated oct 26 2022)
 * 
 * This is the base class for all tower objects
 * 
 * VARIABLES
 * basic values 
 * list of TowerAttributes
 *      TowerAttributes are declared, and their name is assigned in decleration
 *      All other variables should be received from variables adjusted in inspector
 *          And assigned to the TowerAttribute in start
 * one tower attribute(health)
 * a sprite to be displayed in RUIbuild
 * 
 * FUNCTIONS
 * virtual Start()
 *      calls InitializeTowerAttributes
 * virtual Update()
 *      only used in BuildMode, making a tower in BuildMode follow the cursor untill:
 *          a left click is detected
 *              and if this object's collider is not in contact with any others the tower simply
 *              exits build mode and stops following cursor
 *         a right click is detected
 *              which refunds the tower cost to GC score and destroys this tower
 * OnTriggerEnter() and OnTriggerExit()
 *      if in build mode these will keep track of objects that collide with this one, 
 *      so that it cannot be built while colliding with another object
 * virtual InitializeTowerAttributes()
 *      assigns variables adjuested in inspector to appropriate TowerAttributes
 * UpdateHp(float)
 *      takes in a float and adds it to the the hp TowerAttribute
 *      also detects dropping to 0 hp and destroys this tower
 * SellTower()
 *      refunds sellvalue to GC score and destroys this tower
 * UpgradeAttribute(TowerAttribute, int)
 *      first checks to see if there are enough points to buy the upgrade
 *          if not sends msg to alert text
 *      upgrades the attribute, adjusts all attribute values
 *      subtracts int from GC score
 *      
 */
public class TowerBaseClass : MonoBehaviour
{
    #region Variable Declaration
    //these stats are for logic in the code; for ui use the list of tower attributes
    [Header("General Stats")]
    public string towerType;
    public int cost;
    public int sellValue;
    public int upgradeLimit;

    public List<TowerAttribute> attributes = new List<TowerAttribute>();

    [Header("Tower's Stats (Initialization Only)")]
    public TowerAttribute hp = new TowerAttribute("Health",0 ,0 ,0 ,0 ,0);
    public float hpStart;

    [Header("Components")]
    public Sprite buttonSprite;

    //Variables for Build Mode
    public bool buildModeTower = false;
    List<GameObject> collidedObjects = new List<GameObject>();
    #endregion

    public virtual void Start()
    {
        InitializeTowerAttibutes();
    }

    public virtual void Update()
    {
        if (buildModeTower)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
            BoxCollider towerCollider = GetComponent<BoxCollider>();
            towerCollider.isTrigger = true;
            if(Input.GetMouseButtonDown(1))
            {
                UIMain.UI.buildMode = false;
                buildModeTower = false;
                Destroy(gameObject);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if(collidedObjects.Count == 0)
                {
                    GameControllerScript.GC.score -= cost;
                    UIMain.UI.buildMode = false;
                    buildModeTower = false;
                    towerCollider.isTrigger = false;
                }
            }
        }
    }

    #region Detecting Collisions (For Build Mode)
    public void OnTriggerEnter(Collider other)
    {
        if (buildModeTower)
        {
            if(!(other.CompareTag("Floor")) && !(other.CompareTag("Targeting")))
            {
                collidedObjects.Add(other.gameObject);

            }

        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (buildModeTower)
        {
            if(!(other.CompareTag("Floor")) && !(other.CompareTag("Targeting")))
            {
                collidedObjects.Remove(other.gameObject);

            }

        }

    }
    #endregion

    public virtual void InitializeTowerAttibutes()
    {
        hp.start = hpStart;
        hp.current = hpStart;
        hp.upgradeLimit = upgradeLimit;
        attributes.Add(hp);
    }
    
    public void UpdateHp(float damage)
    {
        hp.current -= damage;
        if(hp.current >= hp.start)
        {
            hp.current = hp.start;
        }
        if(hp.current <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SellTower()
    {
        GameControllerScript.GC.score += sellValue;
        Destroy(gameObject);
    }

    public void UpgradeAttribute(TowerAttribute attribute, int cost)
    {
        if(cost > GameControllerScript.GC.score)
        {
            UIMain.UI.SetAlertText("Not Enough Points to Upgrade");
        }
        else
        {
            GameControllerScript.GC.score -= cost;
            attribute.current += attribute.upgradeInc;
            attribute.upgradeCount++;
        }

    }
}
