using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * 
 * this instantiates an object sent from UIscript, then disables itself
 *      the BuildTower() function is called by UIscript
 * it also sets the "parent tile" variable in the buildibleAndAttackable class in the instantiated object to itself,
 *      which handles reactivating this gameobject when the instantiated object is destroyed
 */
public class BuildableTileScript : MonoBehaviour
{
    //builds the selected tower and sets this tile to inactive
    public void BuildTower(GameObject towerToBuild)
    {
        //!!setting the parent looks messy, but sets the parent to the desired gameobject
        //this gameobject must have 2 parent for this to work properly; "BuildableTiles" and then "Towers"
        var newBuildable = Instantiate(towerToBuild, this.transform.position, Quaternion.identity, this.transform.parent.transform.parent.transform);
        newBuildable.GetComponent<BuildableAndAttackableObject>().parentTile = this.gameObject;
        gameObject.SetActive(false);
    }
}
