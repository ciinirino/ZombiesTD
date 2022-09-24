using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * 
 * this is a class for storing information about buildable towers
 * used in UI script
 */
public class BuildTowerInfo : MonoBehaviour
{
    public string towerName;
    public int towerCost;
    public GameObject towerPrefab;
    public int towerDropdownValue;

    public BuildTowerInfo (string newName, int newCost, GameObject newPrefab, int newDropdownValue)
    {
        towerName = newName;
        towerCost = newCost;
        towerPrefab = newPrefab;
        towerDropdownValue = newDropdownValue;
    }   
}
